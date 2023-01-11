using Kursach.Models.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Profile
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public Client Client { get; set; }
        public Worker Worker { get; set; }
    }
}
