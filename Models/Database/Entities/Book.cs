using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Category Category { get; set; }
        public int? CategoryId { get; set; }

        public Publication Publication { get; set; }

        public List<Filter> Filters { get; set; }
        public List<Literature> Literatures { get; set; }

        public Book()
        {
            Filters = new List<Filter>();
            Literatures = new List<Literature>();
        }
    }
}
