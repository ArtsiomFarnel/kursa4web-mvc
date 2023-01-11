using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class WrittenOff
    {
        public int Id { get; set; }
        public string WorkerIdentityNumber { get; set; }
        public DateTime Date { get; set; }


        public Copy Copy { get; set; }
        public int CopyId { get; set; }
    }
}
