using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Books
{
    public class CopyViewModel
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string StorageLocation { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
