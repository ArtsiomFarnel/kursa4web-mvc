using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Debt
    {
        public int Id { get; set; }
        public DateTime ReceivingDate { get; set; }
        public DateTime EstimatedReturnDate { get; set; }
        public bool IsReturned { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }

        public Copy Copy { get; set; }
        public int CopyId { get; set; }
    }
}
