using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Profile
{
    public class DebtViewModel
    {
        public string BookName { get; set; }
        public string BookImage { get; set; }
        public string LibraryName { get; set; }
        public string LibraryAddress { get; set; }
        public DateTime ReceivingDate { get; set; }
        public DateTime EstimatedReturnDate { get; set; }
        public int CopyId { get; set; }
        public int PublicationId { get; set; }
        public int StorageId { get; set; }
        public int RoomId { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
    }
}
