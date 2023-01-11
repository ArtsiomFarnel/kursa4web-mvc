using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public Copy Copy { get; set; }
        public int CopyId { get; set; }

        public Worker Worker { get; set; }
        public int? WorkerId { get; set; }

        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}
