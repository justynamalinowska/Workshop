using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

namespace TimeAndTimePeriod
{
    class Program
    {
        public static void Main(string[] args)
        {
            Time t = new Time("11:23:54");
            Console.WriteLine(t.Minutes);
        }
    }
}