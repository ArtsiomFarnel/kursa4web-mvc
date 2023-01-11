using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Kursach.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    public class ProfileController : Controller
    {
        private DatabaseContext _context;

        public ProfileController(DatabaseContext context)
        {
            _context = context;
        }

        private async Task<User> GetCurrentUser()
        {
            string email = HttpContext.User.Identity.Name;
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        [Authorize(Roles = "user, client")]
        public async Task<IActionResult> ClientProfile()
        {
            User user = await GetCurrentUser();
            Client client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (client == null) return RedirectToAction("AvailableLibraries", "Home");

            ProfileViewModel model = new ProfileViewModel
            {
                User = user ?? null,
                Client = client ?? null
            };
            return View(model);
        }

        [Authorize(Roles = "worker, admin")]
        public async Task<IActionResult> WorkerProfile()
        {
            User user = await GetCurrentUser();
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);

            ProfileViewModel model = new ProfileViewModel
            {
                User = user ?? null,
                Worker = worker ?? null
            };

            return View(model);
        }

        [Authorize(Roles = "user, client")]
        public async Task<IActionResult> DebtList()
        {
            User user = await GetCurrentUser();
            Client client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (client == null) return RedirectToAction("AvailableLibraries", "Home");
            IQueryable<Debt> debts = _context.Debts.Where(d => d.ClientId == client.Id && !d.IsReturned);

            var copies = (from d in debts
                          join c in _context.Copies
                          on d.CopyId equals c.Id
                          join s in _context.Storages
                          on c.StorageId equals s.Id
                          select new
                          {
                              ReceivingDate = d.ReceivingDate,
                              EstimatedReturnDate = d.EstimatedReturnDate,
                              CopyId = c.Id,
                              PublicationId = c.PublicationId,
                              StorageId = s.Id,
                              RoomId = s.RoomId
                          });

            var libraries = (from c in copies
                             join r in _context.Rooms
                             on c.RoomId equals r.Id
                             join l in _context.Libraries
                             on r.LibraryId equals l.Id
                             select new
                             {
                                 ReceivingDate = c.ReceivingDate,
                                 EstimatedReturnDate = c.EstimatedReturnDate,
                                 CopyId = c.CopyId,
                                 PublicationId = c.PublicationId,
                                 StorageId = c.StorageId,
                                 RoomId = r.Id,
                                 LibraryId = l.Id,
                                 LibraryName = l.Name,
                                 LibraryAddress = l.Address
                             });

            var result = (from p in _context.Publications
                          join l in libraries
                          on p.Id equals l.PublicationId
                          select new DebtViewModel
                          {
                              BookName = p.Book.Name,
                              BookImage = p.Book.Image,
                              LibraryName = l.LibraryName,
                              LibraryAddress = l.LibraryAddress,
                              ReceivingDate = l.ReceivingDate,
                              EstimatedReturnDate = l.EstimatedReturnDate,
                              CopyId = l.CopyId,
                              LibraryId = l.LibraryId,
                              PublicationId = p.Id,
                              RoomId = l.RoomId,
                              StorageId = l.StorageId,
                              BookId = p.BookId
                          });

            return View(await result.ToListAsync());
        }

        [Authorize(Roles = "client")]
        public async Task<IActionResult> GetBook(int? bookId)
        {
            if (bookId != null)
            {
                User user = await GetCurrentUser();
                Client client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                if (client == null) return RedirectToAction("AvailableLibraries", "Home");

                var debts = await _context.Debts.FirstOrDefaultAsync(d => d.ClientId == client.Id && d.CopyId == (int)bookId && !d.IsReturned);
                var operations = await _context.Operations.FirstOrDefaultAsync(d => d.ClientId == client.Id && d.CopyId == (int)bookId && d.Status.Equals("in processing"));
                if (debts != null || operations != null) return RedirectToAction("ShowCatalog", "Catalog");

                Operation operation = new Operation
                {
                    ClientId = client.Id,
                    CopyId = (int)bookId,
                    Status = "in processing",
                    Name = "take"
                };

                _context.Operations.Add(operation);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowCatalog", "Catalog");
            }
            else return NotFound();
        }

        [Authorize(Roles = "client")]
        public async Task<IActionResult> ReturnBook(int? bookId)
        {
            if (HttpContext.User.IsInRole("user")) return RedirectToAction("Login", "Account");
            if (bookId != null)
            {
                User user = await GetCurrentUser();
                Client client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                if (client == null) return RedirectToAction("AvailableLibraries", "Home");

                Operation operation = new Operation
                {
                    ClientId = client.Id,
                    CopyId = (int)bookId,
                    Status = "in processing",
                    Name = "return"
                };

                _context.Operations.Add(operation);
                await _context.SaveChangesAsync();

                return RedirectToAction("ShowCatalog", "Catalog");
            }
            else return NotFound();
        }
    }
}
