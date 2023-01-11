using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Library Library { get; set; }
        public int LibraryId { get; set; }

        public List<Operation> Operations { get; set; }
        public List<Debt> Debts { get; set; }

        public Client()
        {
            Operations = new List<Operation>();
            Debts = new List<Debt>();
        }
    }
}
