using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Database.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string UniversityName { get; set; }
        public string FacultyName { get; set; }
        public string GroupName { get; set; }
        public int CourseNumber { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
