using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Schooler
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string ClassName { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
