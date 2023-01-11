using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Clients
{
    public class StudentViewModel
    {
        public ClientViewModel Client { get; set; }
        public Student Student { get; set; }
    }
}
