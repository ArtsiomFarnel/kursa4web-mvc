using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Catalog
{
    public class LocationViewModel
    {
        public int CopyId { get; set; }
        public int CopyAmount { get; set; }
        public int LibraryId { get; set; }
        public int StorageId { get; set; }
        public int RoomId { get; set; }
        public string LibraryName { get; set; }
        public string LibraryAddress { get; set; }
        public string RoomName { get; set; }
    }
}
