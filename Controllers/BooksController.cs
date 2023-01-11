using Kursach.Models.Books;
using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    [Authorize(Roles = "worker")]
    public class BooksController : Controller
    {
        private DatabaseContext _context;

        public BooksController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CopyList(string flag, string fdate, string sdate)
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == worker.RoomId);
            IQueryable<Storage> storages = _context.Storages.Where(s => s.RoomId == room.Id);

            IQueryable<CopyViewModel> copies = null;
            
            if (String.IsNullOrEmpty(flag)) flag = "added";
                if (!String.IsNullOrEmpty(fdate) && !String.IsNullOrEmpty(sdate))
                {
                    //
                    if (flag.Equals("added"))
                    {
                        copies = (from c in _context.Copies
                                  join s in storages
                                  on c.StorageId equals s.Id
                                  where c.ReplenishmentDate.Date > Convert.ToDateTime(fdate).Date && c.ReplenishmentDate.Date < Convert.ToDateTime(sdate).Date
                                  select new CopyViewModel()
                                  {
                                      Date = c.ReplenishmentDate,
                                      Amount = c.Amount,
                                      Id = c.Id,
                                      IdentityNumber = c.IdentityNumber,
                                  });
                    }
                    else
                    {
                        IQueryable<CopyViewModel> temp = (from c in _context.Copies
                                                 join s in storages
                                                 on c.StorageId equals s.Id
                                                          select new CopyViewModel()
                                                          {
                                                              Date = c.ReplenishmentDate,
                                                              Amount = c.Amount,
                                                              Id = c.Id,
                                                              IdentityNumber = c.IdentityNumber,
                                                          });

                        copies = (from w in _context.WrittenOffs
                                  join c in temp
                                  on w.CopyId equals c.Id
                                  where w.Date.Date > Convert.ToDateTime(fdate).Date && w.Date.Date < Convert.ToDateTime(sdate).Date
                                  select new CopyViewModel()
                                  {
                                      Date = w.Date,
                                      Amount = c.Amount,
                                      Id = c.Id,
                                      IdentityNumber = c.IdentityNumber,
                                  });
                    }
                }
                else
                {
                    if (flag.Equals("added"))
                    {
                        copies = (from c in _context.Copies
                                  join s in storages
                                  on c.StorageId equals s.Id
                                  select new CopyViewModel()
                                  {
                                      Date = c.ReplenishmentDate,
                                      Amount = c.Amount,
                                      Id = c.Id,
                                      IdentityNumber = c.IdentityNumber,
                                  });
                    }
                    else
                    {
                        IQueryable<CopyViewModel> temp = (from c in _context.Copies
                                                          join s in storages
                                                          on c.StorageId equals s.Id
                                                          select new CopyViewModel()
                                                          {
                                                              Date = c.ReplenishmentDate,
                                                              Amount = c.Amount,
                                                              Id = c.Id,
                                                              IdentityNumber = c.IdentityNumber,
                                                          });

                        copies = (from w in _context.WrittenOffs
                                  join c in temp
                                  on w.CopyId equals c.Id
                                  select new CopyViewModel()
                                  {
                                      Date = w.Date,
                                      Amount = c.Amount,
                                      Id = c.Id,
                                      IdentityNumber = c.IdentityNumber,
                                  });
                    }
                }
            

            return View(await copies.ToListAsync());
        }

        public async Task<IActionResult> CopyByStorage(int? shelf)
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == worker.RoomId);

            var storage = _context.Storages.Where(s => s.RoomId == room.Id);

            var tempCopies = (from c in _context.Copies
                              join s in _context.Storages
                              on c.StorageId equals s.Id
                              select c);

            var result = (from d in _context.Debts
                          join c in tempCopies
                          on d.CopyId equals c.Id
                          join s in _context.Storages
                          on c.StorageId equals s.Id
                          where !d.IsReturned && (shelf != null ? s.RackNumber == (int)shelf : s.RackNumber == 0)
                          select c);

            return View(await result.ToListAsync());
        }

        public async Task<IActionResult> WriteOffBooks()
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == worker.RoomId);
            IQueryable<Storage> storages = _context.Storages.Where(s => s.RoomId == room.Id);
            var copies = (from c in _context.Copies
                          join s in storages
                          on c.StorageId equals s.Id
                          where !c.IsWrittenOff
                          select new CopyViewModel()
                          {
                              Amount = c.Amount,
                              Id = c.Id,
                              IdentityNumber = c.IdentityNumber,
                              StorageLocation = s.RackNumber.ToString() + "-" + s.ShelfNumber.ToString() + "-" + s.CellNumber.ToString()
                          });
            return View(await copies.ToListAsync());
        }

        public async Task<IActionResult> WriteOffCopy(int? copyId)
        {
            if (copyId == null) return NotFound();
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);

            Copy copy = await _context.Copies.FirstOrDefaultAsync(c => c.Id == (int)copyId);
            copy.Amount = 0;
            copy.IsWrittenOff = true;
            await _context.SaveChangesAsync();

            WrittenOff writtenOff = new WrittenOff
            {
                CopyId = copy.Id,
                WorkerIdentityNumber = worker.IdentityNumber
            };
            _context.WrittenOffs.Add(writtenOff);
            await _context.SaveChangesAsync();

            return RedirectToAction("WriteOffBooks", "Books");
        }

        [HttpGet]
        public async Task<IActionResult> AddNewCopy()
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);

            var rooms = _context.Rooms.Where(r => r.Id == worker.RoomId);
            var storages = await (from s in _context.Storages
                            join r in rooms
                            on s.RoomId equals r.Id
                            select s).ToListAsync();

            SelectList slist = new SelectList(storages, "Id", "Id");
            ViewBag.StorageList = slist;

            var pubs = await _context.Publications.ToListAsync();

            SelectList plist = new SelectList(pubs, "Id", "Id");
            ViewBag.PublicationList = plist;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCopy(Copy copy)
        {
            if (String.IsNullOrEmpty(copy.IdentityNumber) || copy.IdentityNumber.Equals(""))
                copy.IdentityNumber = "temp";
            Copy check = await _context.Copies.FirstOrDefaultAsync(b =>
                b.StorageId == copy.StorageId && b.PublicationId == copy.PublicationId);
            if (check == null)
            {
                _context.Copies.Add(copy);
                await _context.SaveChangesAsync();
            }
            else
            {
                check.Amount += copy.Amount;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("WriteOffBooks", "Books");
        }
    }
}
