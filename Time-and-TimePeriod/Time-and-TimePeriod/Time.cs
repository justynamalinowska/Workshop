using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

namespace TimeAndTimePeriod
{
    /// <summary>
    /// Represents a time with hours, minutes and seconds.
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Gets or sets value of hours, minutes and seconds at certain Time.
        /// </summary>

        public readonly byte Hours;
        public readonly byte Minutes;
        public readonly byte Seconds;

        /// <summary>
        /// Creates a new instance of the Time class with the specified hours, minutes and seconds.
        /// </summary>
        /// <param name="hours">The hours of the Time.</param>
        /// <param name="minutes">The minutes of the Time.</param>
        /// <param name="seconds">The seconds of the Time.</param>

        public Time(byte hours=00, byte minutes=00, byte seconds=00)
        {
            if (hours > 23 ||minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            Hours = Convert.ToByte(hours % 24);
            Minutes = Convert.ToByte(minutes % 60);
            Seconds = Convert.ToByte(seconds % 60);
        }

        public Time()
        {
            Hours = 00;
            Minutes = 00;
            Seconds = 00;
        }

        /// <summary>
        /// Creates a new instance of the Time class with the specified seconds.
        /// </summary>
        /// /// <param name="seconds">The seconds amount of the Time.</param>
        public Time(long seconds)
        {
            if (seconds < 0 || seconds > 86399)
                throw new ArgumentOutOfRangeException();

            Hours = Convert.ToByte(seconds / 3600);
            Minutes = Convert.ToByte((seconds / 60)% 60);
            Seconds = Convert.ToByte(seconds % 60);
        }

        /// <summary>
        /// Creates a new instance of the Time class with the specified hours, minutes and seconds.
        /// </summary>
        /// <param name="text">Parameters input by hh:mm:ss patern.</param>
        public Time(string text) 
        {
            if (text.Length != 8)
                throw new Exception("Wrong input try with this pattern: hh:mm:ss");

            string[] array = text.Split(':');
            byte h = Byte.Parse(array[0]);
            byte m = Byte.Parse(array[1]);
            byte s = Byte.Parse(array[2]);

            if ( h > 23 || m > 59 || s > 59)
                throw new ArgumentOutOfRangeException();
            
            Hours = h;
            Minutes = m;
            Seconds = s;
        }

        /// <summary>
        /// Returns a string that represents the current Time.
        /// </summary>
        /// <returns>A string that represents the current Time.</returns>
        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

        /// <summary>
        /// Returns if two times are equal to each other.
        /// </summary>
        public bool Equals(Time other) => (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);
        public override bool Equals(object? obj)
        {
            if (obj is Time other) return Equals(other);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Hours, Minutes, Seconds);

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !(t1==t2);

        public int CompareTo(Time other) => ((3600 * Hours + 60 * Minutes + Seconds) - (3600 * other.Hours + 60 * other.Minutes + other.Seconds));

        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;
        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;

        public static Time Plus(Time t, Time t1) => new(t.Hours * 3600 + t.Minutes * 60 + t.Seconds + t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds);
        public static Time Plus(Time t, TimePeriod tp) => new(t.Hours * 3600 + t.Minutes * 60 + t.Seconds + tp.TimeLength);
        public Time Plus(Time t) => Plus(this, t);
        public Time Plus(TimePeriod tp) => Plus(this, tp);

        public static Time Minus(Time t, TimePeriod tp) => new((t.Hours * 3600 + t.Minutes * 60 + t.Seconds) - tp.TimeLength);
        public static Time Minus(Time t, Time t1) => new((t.Hours * 3600 + t.Minutes * 60 + t.Seconds) - t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds);
        public Time Minus(TimePeriod tp) => Minus(this, tp);
        public Time Minus(Time t1) => Minus(this, t1);

        public static Time operator +(Time t, TimePeriod tp) => Plus(t, tp);
        public static Time operator +(Time t, Time t1) => Plus(t, t1);
        public static Time operator ++(Time t) => new(t.Hours * 3600 + t.Minutes * 60 + t.Seconds + 1); 
        public static Time operator -(Time t, TimePeriod tp) => Minus(t, tp);
        public static Time operator -(Time t, Time t1) => Minus(t, t1);
        public static Time operator --(Time t) => new(t.Hours * 3600 + t.Minutes * 60 + t.Seconds - 1);
        public static Time operator *(int x, Time t) => new((t.Hours * 3600 + t.Minutes * 60 + t.Seconds) * x);
        public static Time operator /(Time t, int x) => new((t.Hours * 3600 + t.Minutes * 60 + t.Seconds) / x);
    }
}
