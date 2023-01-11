using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Library Library { get; set; }
        public int LibraryId { get; set; }

        public List<Storage> Storages { get; set; }
        public List<Worker> Workers { get; set; }

        public Room()
        {
            Storages = new List<Storage>();
            Workers = new List<Worker>();
        }
    }
}
