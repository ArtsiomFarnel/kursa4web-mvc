using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Administration
{
    public class BooksViewModel
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
    }
}
