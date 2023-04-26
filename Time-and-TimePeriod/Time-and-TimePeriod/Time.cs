using System;
using System.Text;

namespace TimeAndTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
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
        
        public override string ToString()
        {
            return $"{Hours:D}:{Minutes:D}:{Seconds:D}";
        }
        
        public bool Equals(Time other) => (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);
        public override bool Equals(object? obj)
        {
            if (obj is not null) return false;
            if (obj is Time other) return Equals(other);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hours, Minutes, Seconds);
        }

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !(t1==t2);
        
                //public int CompareTo(Time other) => (3600 * Hours + 60 * Minutes + Seconds - (3600 * other.Hours + 60 * other.Minutes + other.Seconds)).Sign;

        public int CompareTo(Time other)
        {
            int t = (3600 * Hours + 60 * Minutes + Seconds - (3600 * other.Hours + 60 * other.Minutes + other.Seconds));
            if (t == 0)
                return 0;
            else if (t > 0)
                return 1;
            else
                return -1;
        }

        public static bool operator <(Time left, Time right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Time left, Time right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Time left, Time right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Time left, Time right)
        {
            return left.CompareTo(right) >= 0;
        }

    }
    }
}
