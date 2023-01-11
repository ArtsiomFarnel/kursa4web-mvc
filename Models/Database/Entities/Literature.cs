using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Literature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
