using System;
using System.Text;

namespace TimeAndTimePeriod
{
    public struct Time
    {
        public readonly byte Hours;
        public readonly byte Minutes;
        public readonly byte Seconds;

        public Time(byte hours, byte minutes, byte seconds)
        {
            if (hours > 23 || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(byte hours, byte minutes)
        {
            if (hours > 23 || minutes > 59)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;
            Seconds = 00;
        }

        public Time(byte hours)
        {
            if (hours > 23)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = 00;
            Seconds = 00;
        }
        public Time()
        {
            Hours = 00;
            Minutes = 00;
            Seconds = 00;
        }

        public Time(string s) // s = hh:mm:ss
        {
            if (s.Length != 8)
                throw new Exception("Wrong input try with the pattern: hh:mm:ss");

            string[] array = s.Split(':');

            Hours = Byte.Parse(array[0]);
            Minutes = Byte.Parse(array[1]);
            Seconds = Byte.Parse(array[2]);
        }

    }
}
