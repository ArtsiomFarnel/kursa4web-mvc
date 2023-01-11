using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public string Email { get; set; }        

        public Role Role { get; set; }
        public int? RoleId { get; set; }

        public Worker Worker { get; set; }
        public Client Client { get; set; }
        public Student Student { get; set; }
        public Scientist Scientist { get; set; }
        public Schooler Schooler { get; set; }
    }
}
