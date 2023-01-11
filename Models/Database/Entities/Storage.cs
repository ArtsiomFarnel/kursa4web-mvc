using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Storage
    {
        public int Id { get; set; }
        public int RackNumber { get; set; }
        public int ShelfNumber { get; set; }
        public int CellNumber { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

        public List<Copy> Copies { get; set; }

        public Storage()
        {
            Copies = new List<Copy>();
        }
    }
}
