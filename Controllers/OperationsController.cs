using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Kursach.Models.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    [Authorize(Roles = "worker")]
    public class OperationsController : Controller
    {
        private DatabaseContext _context;

        public OperationsController(DatabaseContext context)
        {
            _context = context;
        }

        private async Task<User> GetCurrentUser()
        {
            string email = HttpContext.User.Identity.Name;
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<IActionResult> OperationsInProcessing()
        {
            var operations = (from cl in _context.Clients
                              join o in _context.Operations
                              on cl.Id equals o.ClientId
                              join cp in _context.Copies
                              on o.CopyId equals cp.Id
                              where o.Status.Equals("in processing")
                              select new UnconfirmedOperationsViewModel
                              {
                                  ClientIdentityNumber = cl.IdentityNumber,
                                  CopyIdentityNumber = cp.IdentityNumber,
                                  Date = o.Date,
                                  Name = o.Name,
                                  Id = o.Id
                              });

            return View(await operations.ToListAsync());
        }

        public async Task<IActionResult> AcceptOperation(int? id)
        {
            if (id == null) return NotFound();
            User user = await GetCurrentUser();
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);

            Operation operation = await _context.Operations.FirstOrDefaultAsync(o => o.Id == (int)id);
            operation.WorkerId = worker.Id;
            operation.Status = "yes";
            await _context.SaveChangesAsync();

            Copy copy = await _context.Copies.FirstOrDefaultAsync(c => c.Id == operation.CopyId);
            if (operation.Name.Equals("take")) copy.Amount -= 1;
            if (operation.Name.Equals("return")) copy.Amount += 1;
            await _context.SaveChangesAsync();

            Client client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == operation.ClientId);
            if (operation.Name.Equals("take"))
            {
                Debt debt = new Debt
                {
                    IsReturned = false,
                    ClientId = client.Id,
                    CopyId = copy.Id,
                    ReceivingDate = DateTime.Now,
                    EstimatedReturnDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + copy.Days)
                };
                _context.Debts.Add(debt);
                await _context.SaveChangesAsync();
            }
            if (operation.Name.Equals("return"))
            {
                Debt debt = await _context.Debts.FirstOrDefaultAsync(c => c.ClientId == client.Id && c.CopyId == copy.Id);
                debt.IsReturned = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("OperationsInProcessing", "Operations");
        }

        public async Task<IActionResult> RejectOperation(int? id)
        {
            if (id == null) return NotFound();
            User user = await GetCurrentUser();
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);

            Operation operation = await _context.Operations.FirstOrDefaultAsync(o => o.Id == (int)id);
            operation.WorkerId = worker.Id;
            operation.Status = "no";
            await _context.SaveChangesAsync();

            return RedirectToAction("OperationsInProcessing", "Operations");
        }
    }
}
