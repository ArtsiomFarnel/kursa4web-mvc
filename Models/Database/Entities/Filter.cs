using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Filter
    {
        public int Id { get; set; }

        public Book Book { get; set; }
        public int? BookId { get; set; }

        public Genre Genre { get; set; }
        public int? GenreId { get; set; }
    }
}
