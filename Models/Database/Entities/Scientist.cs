using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Scientist
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string ScientificTheme { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
