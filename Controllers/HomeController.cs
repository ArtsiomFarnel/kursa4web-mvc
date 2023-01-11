using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kursach.Models;
using Kursach.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Kursach.Models.Home;
using Microsoft.EntityFrameworkCore;
using Kursach.Models.Database.Entities;

namespace Kursach.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var copies = (from o in _context.Operations
                          join c in _context.Copies
                          on o.CopyId equals c.Id
                          group c by c.Id into g
                          select new
                          {
                              CopyId = g.Key,
                              Popularity = g.Count()
                          });

            var populars = (from c in _context.Copies
                            join g in copies
                            on c.Id equals g.CopyId
                            select new
                            {
                                PublicationId = c.PublicationId,
                                Popularity = g.Popularity
                            });

            var books = (from c in populars
                         join p in _context.Publications
                         on c.PublicationId equals p.Id
                         orderby c.Popularity descending
                         select p);

            var result = (from b in books
                          join p in _context.Books
                          on b.BookId equals p.Id
                          join c in _context.Categories
                          on p.CategoryId equals c.Id
                          select new PopularBooksViewModel
                          {
                              BookName = p.Name,
                              CategoryName = c.Name,
                              Description = p.Description,
                              Id = p.Id,
                              Image = p.Image,
                              PublicationName = b.Name,
                              Year = p.Year
                          }).Take(5);

            return View(await result.ToListAsync());
        }

        public async Task<IActionResult> AvailableLibraries()
        {
            var libs = (from r in _context.Rooms
                        join l in _context.Libraries
                        on r.LibraryId equals l.Id
                        select new AvailableRoomsViewModel
                        {
                            LibraryId = l.Id,
                            Description = l.Description,
                            LibraryName = l.Name,
                            Address = l.Address
                        });
            return View(await libs.ToListAsync());
        }

        [HttpGet]
        public IActionResult Register(int? libId)
        {
            ViewBag.LibId = libId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Address != null)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(HttpContext.User.Identity.Name));
                if (user != null)
                {
                    Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Name.Equals("client"));
                    if (role != null) user.Role = role;
                    await _context.SaveChangesAsync();
                    Client client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (client != null) return RedirectToAction("Index", "Home");

                    _context.Clients.Add(new Client
                    {
                        UserId = user.Id,
                        Address = model.Address,
                        LibraryId = model.LibraryId,
                        IdentityNumber = "client" + user.Id.ToString()
                    });
                    
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else NotFound();
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
