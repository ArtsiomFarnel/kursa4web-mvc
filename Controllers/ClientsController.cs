using Kursach.Models.Clients;
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
    [Authorize(Roles = "worker")]
    public class ClientsController : Controller
    {
        private DatabaseContext _context;

        public ClientsController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Library> GetLibrary()
        {
            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == worker.RoomId);
            return await _context.Libraries.FirstOrDefaultAsync(l => l.Id == room.LibraryId);
        }

        public async Task<IActionResult> ClientsByCategoryStudent(string unName, string facName)
        {
            IQueryable<StudentViewModel> students = null;
            Library library = await GetLibrary();
            var temp = _context.Clients.Where(c => c.LibraryId == library.Id);

            var clients = (from c in temp
                           join u in _context.Users
                           on c.UserId equals u.Id
                           select new ClientViewModel()
                           {
                               UserId = u.Id,
                               Name = u.Name,
                               IdentityNumber = c.IdentityNumber,
                               Address = c.Address,
                               Date = u.DateOfRegistration
                           });

            if (String.IsNullOrEmpty(unName) && String.IsNullOrEmpty(facName))
            {
                students = (from s in _context.Students
                            join c in clients
                            on s.UserId equals c.UserId
                            select new StudentViewModel()
                            {
                                Client = c,
                                Student = s
                            });
            }
            else if (!String.IsNullOrEmpty(unName) || !String.IsNullOrEmpty(facName))
            {
                if (!String.IsNullOrEmpty(unName) && String.IsNullOrEmpty(facName))
                {
                    students = (from s in _context.Students
                                join c in clients
                                on s.UserId equals c.UserId
                                where s.UniversityName.Contains(unName)
                                select new StudentViewModel()
                                {
                                    Client = c,
                                    Student = s
                                });
                }
                else if (String.IsNullOrEmpty(unName) && !String.IsNullOrEmpty(facName))
                {
                    students = (from s in _context.Students
                                join c in clients
                                on s.UserId equals c.UserId
                                where s.FacultyName.Contains(facName)
                                select new StudentViewModel()
                                {
                                    Client = c,
                                    Student = s
                                });
                }
                else
                {
                    students = (from s in _context.Students
                                join c in clients
                                on s.UserId equals c.UserId
                                where s.FacultyName.Contains(facName) &&
                                s.UniversityName.Contains(unName)
                                select new StudentViewModel()
                                {
                                    Client = c,
                                    Student = s
                                });
                }
            }
            return View(await students.ToListAsync());
        }

        public async Task<IActionResult> ClientsByCategorySchooler(string schName, string clName)
        {
            IQueryable<SchoolerViewModel> schoolers = null;
            Library library = await GetLibrary();
            var temp = _context.Clients.Where(c => c.LibraryId == library.Id);

            var clients = (from c in temp
                           join u in _context.Users
                           on c.UserId equals u.Id
                           select new ClientViewModel()
                           {
                               UserId = u.Id,
                               Name = u.Name,
                               IdentityNumber = c.IdentityNumber,
                               Address = c.Address,
                               Date = u.DateOfRegistration
                           });
            if (String.IsNullOrEmpty(schName) && String.IsNullOrEmpty(clName))
            {
                schoolers = (from s in _context.Schoolers
                            join c in clients
                            on s.UserId equals c.UserId
                            select new SchoolerViewModel()
                            {
                                Client = c,
                                Schooler = s
                            });
            }
            else if (!String.IsNullOrEmpty(schName) || !String.IsNullOrEmpty(clName))
            {
                if (!String.IsNullOrEmpty(schName) && String.IsNullOrEmpty(clName))
                {
                    schoolers = (from s in _context.Schoolers
                                 join c in clients
                                on s.UserId equals c.UserId
                                where s.SchoolName.Contains(schName)
                                select new SchoolerViewModel()
                                {
                                    Client = c,
                                    Schooler = s
                                });
                }
                else if (String.IsNullOrEmpty(schName) && !String.IsNullOrEmpty(clName))
                {
                    schoolers = (from s in _context.Schoolers
                                 join c in clients
                                on s.UserId equals c.UserId
                                where s.ClassName.Contains(clName)
                                select new SchoolerViewModel()
                                {
                                    Client = c,
                                    Schooler = s
                                });
                }
                else
                {
                    schoolers = (from s in _context.Schoolers
                                 join c in clients
                                on s.UserId equals c.UserId
                                where s.ClassName.Contains(clName) &&
                                s.SchoolName.Contains(schName)
                                select new SchoolerViewModel()
                                {
                                    Client = c,
                                    Schooler = s
                                });
                }
            }
            return View(await schoolers.ToListAsync());
        }

        public async Task<IActionResult> ClientsByCategoryScientist(string orgName, string thName)
        {
            IQueryable<ScientistViewModel> scientists = null;
            Library library = await GetLibrary();
            var temp = _context.Clients.Where(c => c.LibraryId == library.Id);

            var clients = (from c in temp
                           join u in _context.Users
                           on c.UserId equals u.Id
                           select new ClientViewModel()
                           {
                               UserId = u.Id,
                               Name = u.Name,
                               IdentityNumber = c.IdentityNumber,
                               Address = c.Address,
                               Date = u.DateOfRegistration
                           });
            if (String.IsNullOrEmpty(orgName) && String.IsNullOrEmpty(thName))
            {
                scientists = (from s in _context.Scientists
                             join c in clients
                             on s.UserId equals c.UserId
                             select new ScientistViewModel()
                             {
                                 Client = c,
                                 Scientist = s
                             });
            }
            else if (!String.IsNullOrEmpty(orgName) || !String.IsNullOrEmpty(thName))
            {
                if (!String.IsNullOrEmpty(orgName) && String.IsNullOrEmpty(thName))
                {
                    scientists = (from s in _context.Scientists
                                  join c in clients
                                on s.UserId equals c.UserId
                                 where s.OrganizationName.Contains(orgName)
                                 select new ScientistViewModel()
                                 {
                                     Client = c,
                                     Scientist = s
                                 });
                }
                else if (String.IsNullOrEmpty(orgName) && !String.IsNullOrEmpty(thName))
                {
                    scientists = (from s in _context.Scientists
                                  join c in clients
                                on s.UserId equals c.UserId
                                 where s.ScientificTheme.Contains(thName)
                                 select new ScientistViewModel()
                                 {
                                     Client = c,
                                     Scientist = s
                                 });
                }
                else
                {
                    scientists = (from s in _context.Scientists
                                  join c in clients
                                    on s.UserId equals c.UserId
                                 where s.ScientificTheme.Contains(thName) &&
                                 s.OrganizationName.Contains(orgName)
                                 select new ScientistViewModel()
                                 {
                                     Client = c,
                                     Scientist = s
                                 });
                }
            }
            return View(await scientists.ToListAsync());
        }

        public async Task<IActionResult> ClientsByBookOrPublication(string flag, string search)
        {
            IQueryable<ClientViewModel> clients = null;
            IQueryable<Client> peoples = null;

            if (String.IsNullOrEmpty(flag)) flag = "book";
            if (String.IsNullOrEmpty(search))
            {
                var debts = (from c in _context.Clients
                            join d in _context.Debts
                            on c.Id equals d.ClientId
                            group d by d.ClientId into g
                            select new
                            {
                                g.Key
                            });
                peoples = (from c in _context.Clients
                                join d in debts
                                on c.Id equals d.Key
                                select c);
            }
            else
            {
                IQueryable<Copy> copies = _context.Copies;
                if (flag == "book")
                {
                    Book book = await _context.Books.FirstOrDefaultAsync(b => b.Name.Contains(search));
                    if (book != null)
                    {
                        Publication publication = await _context.Publications.FirstOrDefaultAsync(p => p.BookId == book.Id);
                        copies = copies.Where(c => c.PublicationId == publication.Id);
                    }
                }
                else
                {
                    Publication publication =  await _context.Publications.FirstOrDefaultAsync(p => p.Name.Contains(search));
                    if (publication != null)
                    {
                        copies = copies.Where(c => c.PublicationId == publication.Id);
                    }
                }
                var debts = (from d in _context.Debts
                             join c in copies
                             on d.CopyId equals c.Id
                             group d by d.ClientId into g
                             select new
                             {
                                 g.Key
                             });
                peoples = (from c in _context.Clients
                           join d in debts
                           on c.Id equals d.Key
                           select c);
            }
            clients = (from c in peoples
                       join u in _context.Users
                       on c.UserId equals u.Id
                       select new ClientViewModel()
                       {
                           UserId = u.Id,
                           Name = u.Name,
                           IdentityNumber = c.IdentityNumber,
                           Address = c.Address,
                           Date = u.DateOfRegistration
                       });
            return View(await clients.ToListAsync());
        }

        public async Task<IActionResult> ClientsByDateAndBook(string search, string fdate, string sdate)
        {
            IQueryable<ClientViewModel> clients = null;
            Publication publication = null;
            IQueryable<Client> people = _context.Clients;
            if ((fdate == null || sdate == null) && search == null)
            {
                 people = (from c in people
                           join t in (from o in _context.Operations
                                      group o by o.ClientId into g
                                      select new
                                      {
                                          g.Key
                                      })
                           on c.Id equals t.Key
                           select c);
                
            }
            else if ((fdate == null || sdate == null) && search != null)
            {
                Book book = await _context.Books.FirstOrDefaultAsync(b => b.Name.Contains(search));
                if (book != null)
                {
                    publication = await _context.Publications.FirstOrDefaultAsync(p => p.BookId == book.Id);
                    IQueryable<Copy> copies = _context.Copies.Where(c => c.PublicationId == publication.Id);
                    people = (from c in people
                              join t in (from o in _context.Operations
                                         join c in copies
                                         on o.CopyId equals c.Id
                                         group o by o.ClientId into g
                                         select new
                                         {
                                             g.Key
                                         })
                              on c.Id equals t.Key
                              select c);
                }
                else
                {
                    people = (from c in people
                              join t in (from o in _context.Operations
                                         group o by o.ClientId into g
                                         select new
                                         {
                                             g.Key
                                         })
                              on c.Id equals t.Key
                              select c);
                }
            }
            else
            {
                Book book = await _context.Books.FirstOrDefaultAsync(b => b.Name.Contains(search));
                if (book != null)
                {
                    publication = await _context.Publications.FirstOrDefaultAsync(p => p.BookId == book.Id);
                    IQueryable<Copy> copies = _context.Copies.Where(c => c.PublicationId == publication.Id);
                    people = (from c in people
                              join t in (from o in _context.Operations
                                         join c in copies
                                         on o.CopyId equals c.Id
                                         where o.Date.Date >= Convert.ToDateTime(fdate).Date && o.Date.Date <= Convert.ToDateTime(sdate).Date
                                         group o by o.ClientId into g
                                         select new
                                         {
                                             g.Key
                                         })
                              on c.Id equals t.Key
                              select c);
                }
                else
                {
                    people = (from c in people
                              join t in (from o in _context.Operations
                                         group o by o.ClientId into g
                                         select new
                                         {
                                             g.Key
                                         })
                              on c.Id equals t.Key
                              select c);
                }
            }
            clients = (from u in _context.Users
                       join p in people
                       on u.Id equals p.UserId
                       select new ClientViewModel()
                       {
                           UserId = u.Id,
                           IdentityNumber = p.IdentityNumber,
                           Address = p.Address,
                           Date = u.DateOfRegistration,
                           Name = u.Name
                       });
            ViewBag.Publication = publication;
            return View(await clients.ToListAsync());
        }

        public async Task<IActionResult> ClientsByDateAndOperations(string fdate, string sdate)
        {
            IQueryable<Client> clients = (from c in _context.Clients
                                                   where !_context.Operations.Any(op => op.ClientId == c.Id)
                                                   select c);
            if (fdate != null && sdate != null)
            {
                clients = (from c in _context.Clients
                           where !_context.Operations.Any(op => op.ClientId == c.Id && 
                           (op.Date.Date < Convert.ToDateTime(fdate).Date || op.Date.Date > Convert.ToDateTime(sdate).Date))
                           select c);
            }

            var res = (from u in _context.Users
                       join c in clients
                       on u.Id equals c.UserId
                       select new ClientViewModel()
                       {
                           Address = c.Address,
                           Date = u.DateOfRegistration,
                           IdentityNumber = c.IdentityNumber,
                           Name = u.Name,
                           UserId = u.Id
                       });
            return View(await res.ToListAsync());
        }

        public async Task<IActionResult> Debtors()
        {
            IQueryable<Debt> allDebts = _context.Debts;

            string name = HttpContext.User.Identity.Name;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(name));
            Worker worker = await _context.Workers.FirstOrDefaultAsync(w => w.UserId == user.Id);
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == worker.RoomId);
            Library library = await _context.Libraries.FirstOrDefaultAsync(l => l.Id == room.LibraryId);
            var storage = _context.Storages.Where(s => s.RoomId == room.Id);

            var tempCopies = (from c in _context.Copies
                              join s in _context.Storages
                              on c.StorageId equals s.Id
                              select c);
            var tempClients = _context.Clients.Where(c => c.LibraryId == library.Id);

            var copies = (from c in tempCopies
                          join o in allDebts
                          on c.Id equals o.CopyId
                          select c);
            var clients = (from c in tempClients
                           join o in allDebts
                           on c.Id equals o.ClientId
                           select c);

            var debts = (from cp in copies
                              join o in allDebts
                              on cp.Id equals o.CopyId
                              join cl in clients
                              on o.ClientId equals cl.Id
                              where !o.IsReturned && o.EstimatedReturnDate.Date < DateTime.Now
                              select new DebtorViewModel()
                              {
                                  Copy = cp,
                                  Client = cl,
                                  LastDate = o.EstimatedReturnDate,
                                  
                              });

            return View(await debts.ToListAsync());
        }
    }
}
