using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Clients
{
    public class DebtorViewModel
    {
        public Client Client { get; set; }
        public Copy Copy { get; set; }
        public DateTime LastDate { get; set; }
    }
}
