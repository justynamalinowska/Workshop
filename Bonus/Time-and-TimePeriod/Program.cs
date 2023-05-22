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
            // tworzenie obiektów TimePeriod
            var tp1 = new TimePeriod(73212);
            var tp2 = new TimePeriod(48, 13, 55, 56);
            var tp3 = new TimePeriod(29, 58);
            var tp4 = new TimePeriod(128531);
            var t = new Time(10, 20, 30, 40);

            // tworzenie obiektów Time i obliczanie TimePeriod z różnicy
            var startTime = new Time(8, 30);
            var endTime = new Time(16, 45, 30);
            var tp5 = new TimePeriod(startTime, endTime);
            Console.WriteLine(startTime);
            Console.WriteLine(endTime);

            // tworzenie obiektu TimePeriod z napisu
            var tp6 = new TimePeriod("36:27:12:034");

            // wyświetlanie obiektów TimePeriod
            Console.WriteLine(tp1.ToString()); 
            Console.WriteLine(tp2.ToString()); 
            Console.WriteLine(tp3.ToString());
            Console.WriteLine(tp4.ToString());
            Console.WriteLine(tp5.ToString()); 
            Console.WriteLine(tp6.ToString()); 
            Console.WriteLine(t / 2);
        }
    }
}