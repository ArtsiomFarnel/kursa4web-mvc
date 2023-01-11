using Kursach.Models.Administration;
using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private DatabaseContext _context;

        public AdministrationController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UserList()
        {
            var users = (from u in _context.Users
                              select new UsersViewModel()
                              {
                                  Id = u.Id,
                                  Name = u.Name,
                                  Password = u.Password,
                                  DateOfRegistration = u.DateOfRegistration,
                                  Role = u.Role.Name
                              });
            return View(await users.ToListAsync());
        }

        public async Task<IActionResult> BookList()
        {
            var books = (from p in _context.Publications
                         join c in _context.Copies
                         on p.Id equals c.PublicationId
                         select new BooksViewModel
                         {
                             BookId = p.Book.Id,
                             PublicationId = p.Id,
                             Id = c.Id,
                             Amount = c.Id
                         });
            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> OperationList()
        {
            var operations = (from op in _context.Operations
                             select new OperationsViewModel()
                             {
                                 Id = op.Id,
                                 Name = op.Name,
                                 Date = op.Date,
                                 Client = op.Client.IdentityNumber,
                                 Worker = op.Worker.IdentityNumber,
                                 Copy = op.Copy.IdentityNumber
                             });
            return View(await operations.ToListAsync());
        }

        public async Task<IActionResult> LibraryList()
        {
            var libraries = (from lib in _context.Libraries
                              select new LibrariesViewModel()
                              {
                                  Id = lib.Id,
                                  Name = lib.Name,
                                  Address = lib.Address
                              });
            return View(await libraries.ToListAsync());
        }
    }
}
