using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Filter> Filters { get; set; }

        public Genre()
        {
            Filters = new List<Filter>();
        }
    }
}
