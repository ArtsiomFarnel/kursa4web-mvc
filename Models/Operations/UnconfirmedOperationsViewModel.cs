using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Operations
{
    public class UnconfirmedOperationsViewModel
    {
        public string ClientIdentityNumber { get; set; }
        public string CopyIdentityNumber { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
    }
}
