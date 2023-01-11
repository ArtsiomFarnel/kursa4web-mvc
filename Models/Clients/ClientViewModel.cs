using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Clients
{
    public class ClientViewModel
    {
        public int UserId { get; set; }
        public string IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
    }
}
