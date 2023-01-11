using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    [Authorize(Roles = "worker")]
    public class ReportController : Controller
    {
        private DatabaseContext _context;

        public ReportController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Preview()
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            IQueryable<Operation> operations = _context.Operations.Where(o => o.WorkerId == worker.Id && !o.Status.Equals("in processing"));

            return View(await operations.ToListAsync());
        }

        public IActionResult CreateReport(string html)
        {
            html = html.Replace("StrTag", "<").Replace("EndTag", ">");
            HtmlToPdf pdf = new HtmlToPdf();
            PdfDocument document = pdf.ConvertHtmlString(html);
            byte[] file = document.Save();
            document.Close();
            return File(file, "application/pdf", "Report.pdf");
        }
    }
}
