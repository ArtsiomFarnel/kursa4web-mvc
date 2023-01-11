using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Catalog
{
    public class DescriptionViewModel
    {
        public Book Book { get; set; }
        public Publication Publication { get; set; }
        public IEnumerable<AboutBookViewModel> AboutBooks { get; set; }
    }
}
