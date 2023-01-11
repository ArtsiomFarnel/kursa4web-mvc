using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Catalog
{
    public class CatalogItemViewModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string BookName { get; set; }
        public string PublicationName { get; set; }
        public string CategoryName { get; set; }
    }
}
