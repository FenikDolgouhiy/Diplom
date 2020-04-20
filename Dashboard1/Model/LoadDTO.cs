using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Model
{
    public class LoadDTO
    {
        public int Id { get; set; }
        public string Teacher { get; set; }
        public string Subject { get; set; }
        public string Group { get; set; }
        public int TotalHours { get; set; }
        public int Weeks { get; set; }
        public int HoursPerWeek { get; set; }
        public int DaysOfPractice { get; set; }
    }
}
