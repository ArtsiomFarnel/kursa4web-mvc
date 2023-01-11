using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Copy
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public int Amount { get; set; }
        public int Days { get; set; }
        public DateTime ReplenishmentDate { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsWrittenOff { get; set; }

        public Publication Publication { get; set; }
        public int PublicationId { get; set; }

        public Storage Storage { get; set; }
        public int StorageId { get; set; }

        public WrittenOff WrittenOff { get; set; }

        public List<Operation> Operations { get; set; }
        public List<Debt> Debts { get; set; }

        public Copy()
        {
            Operations = new List<Operation>();
            Debts = new List<Debt>();
        }
    }
}
