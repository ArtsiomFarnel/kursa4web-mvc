using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public List<Copy> Copies { get; set; }

        public Publication()
        {
            Copies = new List<Copy>();
        }
    }
}
