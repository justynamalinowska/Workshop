using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TimeAndTimePeriod
{
	public readonly struct TimePeriod : IEquatable<TimePeriod>
	{
        public readonly long TimeLength;

        public TimePeriod(long seconds)
        {
            if (seconds < 0)
                throw new ArgumentException("Seeconds cannot be minus!");
            TimeLength = seconds;
        }

        public TimePeriod(byte hours = 00, byte minutes = 00, byte seconds = 00)
        {
            if (hours > byte.MaxValue || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            TimeLength = 3600 * hours + 60 * minutes + seconds;
        }

        public TimePeriod(Time t1, Time t2)
        {
            TimeLength = Math.Abs(GetTimeLength(t1) - GetTimeLength(t2));
        }

        public long GetTimeLength(Time t) => t.Hours * 3600 + t.Minutes * 60 + t.Seconds; 

        public TimePeriod()
        {
            TimeLength = 0;
        }

        public TimePeriod(string text)
        {
            if (text.Length < 7)
                throw new Exception("Wrong input try with this pattern: hh:mm:ss");

            string[] array = text.Split(':');
            var h = long.Parse(array[0]);
            var m = long.Parse(array[1]);
            var s = long.Parse(array[2]);

            if (m > 59 || s > 59)
                throw new ArgumentOutOfRangeException();

            TimeLength = h * 3600 + m * 60 + s;
        }
        public override string ToString() => $"{(TimeLength/3600):D2}:{((TimeLength/60)%60):D2}:{(TimeLength%60):D2}";
		
	public bool Equals(TimePeriod other) => TimeLength == other.TimeLength;
        public override bool Equals(object? obj)
        {
            if (obj is TimePeriod other) return Equals(other);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(TimeLength);

        public static bool operator ==(TimePeriod t1, TimePeriod t2) => t1.Equals(t2);
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !(t1 == t2);
    }
}
