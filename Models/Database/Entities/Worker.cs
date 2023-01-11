using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

        public List<Operation> Operations { get; set; }

        public Worker()
        {
            Operations = new List<Operation>();
        }
    }
}
