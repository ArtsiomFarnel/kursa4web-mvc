using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Kursach.Models.Management;
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
    public class ManagementController : Controller
    {
        private DatabaseContext _context;

        public ManagementController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ClientsByWorker(string worker, string fdate, string sdate)
        {
            IQueryable<ClientsByWorkerViewModel> clients = null;
            if ((fdate == null || sdate == null) && worker == null)
            {
                clients = (from c in _context.Clients
                           join t in (from o in _context.Operations
                                      group o by o.ClientId into g
                                      select new
                                      {
                                          g.Key
                                      })
                           on c.Id equals t.Key
                           select new ClientsByWorkerViewModel()
                           {
                               Id = c.Id,
                               IdentityNumber = c.IdentityNumber,
                               Name = c.User.Name
                           });
            }
            else if ((fdate == null || sdate == null) && worker != null)
            {
                clients = (from c in _context.Clients
                           join t in (from o in _context.Operations
                                      where o.Worker.IdentityNumber.Contains(worker)
                                      group o by o.ClientId into g
                                      select new
                                      {
                                          g.Key
                                      })
                           on c.Id equals t.Key
                           select new ClientsByWorkerViewModel()
                           {
                               Id = c.Id,
                               IdentityNumber = c.IdentityNumber,
                               Name = c.User.Name
                           });
            }
            else
            {
                clients = (from c in _context.Clients
                           join t in (from o in _context.Operations
                                      where o.Worker.IdentityNumber.Contains(worker) &&
                                      (o.Date.Date >= Convert.ToDateTime(fdate).Date && o.Date.Date <= Convert.ToDateTime(sdate).Date)
                                      group o by o.ClientId into g
                                      select new
                                      {
                                          g.Key
                                      })
                           on c.Id equals t.Key
                           select new ClientsByWorkerViewModel()
                           {
                               Id = c.Id,
                               IdentityNumber = c.IdentityNumber,
                               Name = c.User.Name
                           });
            }
            
            return View(await clients.ToListAsync());
        }

        public async Task<IActionResult> WorkersByRoom(string lib, string room)
        {
            IQueryable<WorkersByRoomViewModel> workers = null;
            if (lib == null && room == null)
                workers = (from w in _context.Workers
                           select new WorkersByRoomViewModel()
                           {
                               Id = w.Id,
                               Name = w.User.Name,
                               IdentityNumber = w.IdentityNumber
                           });
            else if (lib != null)
                workers = (from w in _context.Workers
                           where w.Room.Library.Name.Contains(lib)
                           select new WorkersByRoomViewModel()
                           {
                               Id = w.Id,
                               Name = w.User.Name,
                               IdentityNumber = w.IdentityNumber
                           });
            else if (room != null)
                workers = (from w in _context.Workers
                           where w.Room.Name.Contains(room)
                           select new WorkersByRoomViewModel()
                           {
                               Id = w.Id,
                               Name = w.User.Name,
                               IdentityNumber = w.IdentityNumber
                           });
            return View(await workers.ToListAsync());
        }

        public IActionResult Report(string fdate, string sdate)
        {
            int clientsCount;
            if (fdate == null || sdate == null)
            {
                var res = _context.Operations.Select(r => r.ClientId).Distinct();
                clientsCount = res.Count();
            }
            else
            {
                IQueryable<Operation> op = _context.Operations.Where(o => o.Date.Date >= Convert.ToDateTime(fdate).Date && o.Date.Date <= Convert.ToDateTime(sdate).Date);
                var res = op.Select(r => r.ClientId).Distinct();
                clientsCount = res.Count();
            }
            ViewBag.Clients = clientsCount;
            return View();
        }
    }
}
