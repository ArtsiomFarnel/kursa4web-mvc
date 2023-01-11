using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Home
{
    public class AvailableRoomsViewModel
    {
        public int LibraryId { get; set; }
        public string Description { get; set; }
        public string LibraryName { get; set; }
        public string Address { get; set; }
    }
}
