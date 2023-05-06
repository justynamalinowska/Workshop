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
    /// Represents a time with hours, minutes, seconds and miliseconds.
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Gets or sets value of hours, minutes, seconds and miliseconds. at certain Time.
        /// </summary>

        public readonly byte Hours;
        public readonly byte Minutes;
        public readonly byte Seconds;
        public readonly byte Miliseconds;

        /// <summary>
        /// Creates a new instance of the Time class with the specified hours, minutes, seconds and miliseconds.
        /// </summary>
        /// <param name="hours">The hours of the Time.</param>
        /// <param name="minutes">The minutes of the Time.</param>
        /// <param name="seconds">The seconds of the Time.</param>
        /// <param name="miliseconds">The seconds of the Time.</param>

        public Time(byte hours=00, byte minutes=00, byte seconds=00, byte miliseconds=00)
        {
            if (hours > 23 ||minutes > 59 || seconds > 59 || miliseconds > 999)
                throw new ArgumentOutOfRangeException();

            Hours = Convert.ToByte(hours % 24);
            Minutes = Convert.ToByte(minutes % 60);
            Seconds = Convert.ToByte(seconds % 60);
            Miliseconds = Convert.ToByte(miliseconds % 1000);
        }

        public Time()
        {
            Hours = 00;
            Minutes = 00;
            Seconds = 00;
            Miliseconds = 00;
        }

        /// <summary>
        /// Creates a new instance of the Time class with the specified miliseconds.
        /// </summary>
        /// /// <param name="miliseconds">The seconds amount of the Time.</param>
        public Time(long miliseconds)
        {
            if (miliseconds < 0 || miliseconds > 86399999)
                throw new ArgumentOutOfRangeException();

            Hours = Convert.ToByte(miliseconds / 3600000);
            Minutes = Convert.ToByte((miliseconds / 60000)% 60);
            Seconds = Convert.ToByte((miliseconds / 1000) % 60);
            Miliseconds = Convert.ToByte(miliseconds % 1000);
        }

        /// <summary>
        /// Creates a new instance of the Time class with the specified hours, minutes, seconds and miliseconds.
        /// </summary>
        /// <param name="text">Parameters input by hh:mm:ss patern.</param>
        public Time(string text) 
        {
            if (text.Length != 12)
                throw new Exception("Wrong input try with this pattern: hh:mm:ss:mmm");

            string[] array = text.Split(':');
            byte h = Byte.Parse(array[0]);
            byte m = Byte.Parse(array[1]);
            byte s = Byte.Parse(array[2]);
            byte ms = Byte.Parse(array[3]);

            if ( h > 23 || m > 59 || s > 59 || ms > 999)
                throw new ArgumentOutOfRangeException();
            
            Hours = h;
            Minutes = m;
            Seconds = s;
            Miliseconds = ms;
        }

        /// <summary>
        /// Returns a string that represents the current Time.
        /// </summary>
        /// <returns>A string that represents the current Time.</returns>
        public override string ToString() => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}:{Miliseconds:D3}";

        /// <summary>
        /// Returns if two times are equal to each other.
        /// </summary>
        public bool Equals(Time other) => (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds && Miliseconds == other.Miliseconds);
        public override bool Equals(object? obj)
        {
            if (obj is Time other) return Equals(other);

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(Hours, Minutes, Seconds, Miliseconds);

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !(t1==t2);

        public int CompareTo(Time other) => ((3600000 * Hours + 60000 * Minutes + 1000 * Seconds + Miliseconds) - (3600000 * other.Hours + 60000 * other.Minutes + 1000 * other.Seconds + Miliseconds));

        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;
        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;

        public static Time Plus(Time t, Time t1) => new(t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds + t1.Hours * 3600000 + t1.Minutes * 60000 + 1000 * t1.Seconds + t1.Miliseconds);
        public static Time Plus(Time t, TimePeriod tp) => new(t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds + tp.TimeLength);
        public Time Plus(Time t) => Plus(this, t);
        public Time Plus(TimePeriod tp) => Plus(this, tp);

        public static Time Minus(Time t, TimePeriod tp) => new((t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds) - tp.TimeLength);
        public static Time Minus(Time t, Time t1) => new((t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds) - t1.Hours * 3600000 + t1.Minutes * 60000 + 1000 * t1.Seconds + t1.Miliseconds);
        public Time Minus(TimePeriod tp) => Minus(this, tp);
        public Time Minus(Time t1) => Minus(this, t1);

        public static Time operator +(Time t, TimePeriod tp) => Plus(t, tp);
        public static Time operator +(Time t, Time t1) => Plus(t, t1);
        public static Time operator ++(Time t) => new(t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds + 1); 
        public static Time operator -(Time t, TimePeriod tp) => Minus(t, tp);
        public static Time operator -(Time t, Time t1) => Minus(t, t1);
        public static Time operator --(Time t) => new(t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds - 1);
        public static Time operator *(int x, Time t) => new((t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds) * x);
        public static Time operator /(Time t, int x) => new((t.Hours * 3600000 + t.Minutes * 60000 + 1000 * t.Seconds + t.Miliseconds) / x);
    }
}
