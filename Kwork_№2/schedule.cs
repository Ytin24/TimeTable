using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwork__2
{
    public class schedule
    {
        public int DayOfWeek { get; set; }
        public scheduleSubject subject { get; set; }
    }
    public class scheduleSubject
{
        public string[] NameSubject { get; set; }
    }
}
