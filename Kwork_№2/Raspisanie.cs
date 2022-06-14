using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwork__2
{
    internal class Raspisanie
    {
        public int ДеньНедели { get; set; }
        public RaspisanieDay Day { get; set; }
    }
    public class RaspisanieDay
    {
        public string[] НазваниеПредмета { get; set; }
    }
}
