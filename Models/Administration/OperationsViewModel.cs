using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Models.Administration
{
    public class OperationsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Worker { get; set; }
        public string Copy { get; set; }
    }
}
