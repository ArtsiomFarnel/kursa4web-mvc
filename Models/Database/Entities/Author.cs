using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Literature> Literatures { get; set; }

        public Author()
        {
            Literatures = new List<Literature>();
        }
    }
}
