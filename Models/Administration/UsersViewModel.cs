using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Administration
{
    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
