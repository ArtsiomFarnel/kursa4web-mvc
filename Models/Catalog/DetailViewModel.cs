using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Catalog
{
    public class DetailViewModel
    {
        public DescriptionViewModel Description { get; set; }
        public IEnumerable<LocationViewModel> Locations { get; set; }
    }
}
