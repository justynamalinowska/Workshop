﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TimeAndTimePeriod
{
    /// <summary>
    /// Represents a Time Period in seconds.
    /// </summary>
	public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /// <summary>
        /// Gets or sets value seconds in Time Period.
        /// </summary>
        private readonly long TimeLength;

        /// <summary>
        /// External representation of Time Period in h:m:s pattern.
        /// </summary>

        public long Hours { get { return TimeLength / 3600; } }
        public byte Minutes { get { return (byte)(TimeLength / 60 % 60); } }
        public byte Seconds { get { return (byte)(TimeLength % 60); } }
        public string Value { get { return $"{Hours}:{Minutes}:{Seconds}"; } }


        /// <summary>
        /// Creates a new instance of the TimePeriod class with the specified amount of seconds.
        /// </summary>
        /// /// <param name="Totalseconds">Amount of seconds of the Period.</param>
        public TimePeriod(long Totalseconds = 0)
        {
            if (Totalseconds < 0)
                throw new ArgumentOutOfRangeException("Seeconds cannot be minus!");
            TimeLength = Totalseconds;
        }

        /// <summary>
        /// Creates a new instance of the TimePeriod class with the specified amount of hours, minutes and seconds.
        /// </summary>
        /// <param name="hours">Amount of hours of the Period.</param>
        /// <param name="minutes">Amount of minutes of the Period.</param>
        /// <param name="seconds">Amount of seconds of the Period.</param>
        public TimePeriod(byte hours = 00, byte minutes = 00, byte seconds = 00)
        {
            if (hours > byte.MaxValue || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();

            TimeLength = 3600 * hours + 60 * minutes + seconds;
        }

        /// <summary>
        /// Creates a new instance of the TimePeriod class as the difference of two times.
        /// </summary>
        /// <param name="t1">First object of Time class.</param>
        /// <param name="t2">Second object of Time class</param>
        public TimePeriod(Time t1, Time t2)
        {
            TimeLength = Math.Abs(GetTimeLength(t1) - GetTimeLength(t2));
        }

        /// <summary>
        /// Returns a time like a length of the Time Period.
        /// </summary>
        public long GetTimeLength(Time t) => t.Hours * 3600 + t.Minutes * 60 + t.Seconds;

        /// <summary>
        /// Creates default instance of the TimePeriod class with the zero seconds.
        /// </summary>
        public TimePeriod()
        {
            TimeLength = 0;
        }

        /// <summary>
        /// Creates a new instance of the TimePeriod class with the specified amount of hours, minutes and seconds.
        /// </summary>
        /// <param name="text">Parameters input by h:mm:ss patern.</param>
        public TimePeriod(string text)
        {
            if (text.Length < 7)
                throw new Exception("Wrong input try with this pattern: h:mm:ss");

            string[] array = text.Split(':');
            var h = long.Parse(array[0]);
            var m = long.Parse(array[1]);
            var s = long.Parse(array[2]);

            if (m > 59 || s > 59)
                throw new ArgumentOutOfRangeException();

            TimeLength = h * 3600 + m * 60 + s;
        }

        /// <summary>
        /// Returns a string that represents a Time Period.
        /// </summary>
        /// <returns>A string that represents a Time Period.</returns>
        public override string ToString() => $"{(TimeLength/3600):D2}:{((TimeLength/60)%60):D2}:{(TimeLength%60):D2}";

        /// <returns>
        /// Returns if two time period are equal to each other.
        /// </returns>
        public bool Equals(TimePeriod other) => TimeLength == other.TimeLength;
        public override bool Equals(object? obj)
        {
            if (obj is TimePeriod other) return Equals(other);

            return false;
        }

        /// <returns>
        /// Hash code from combining number of hours, minutes and seconds of this Time object
        /// </returns>
        public override int GetHashCode() => HashCode.Combine(TimeLength);

        public static bool operator ==(TimePeriod t1, TimePeriod t2) => t1.Equals(t2);
        public static bool operator !=(TimePeriod t1, TimePeriod t2) => !(t1 == t2);

        public int CompareTo(TimePeriod other) => (int)(TimeLength - other.TimeLength);

        public static bool operator <(TimePeriod left, TimePeriod right) => left.CompareTo(right) < 0;
        public static bool operator <=(TimePeriod left, TimePeriod right) => left.CompareTo(right) <= 0;
        public static bool operator >(TimePeriod left, TimePeriod right) => left.CompareTo(right) > 0;
        public static bool operator >=(TimePeriod left, TimePeriod right) => left.CompareTo(right) >= 0;

        public TimePeriod Plus(TimePeriod other) => new(TimeLength + other.TimeLength);
        public static TimePeriod Plus(TimePeriod t1, TimePeriod t2) => new(t1.TimeLength + t2.TimeLength);

        public TimePeriod Minus(TimePeriod other) => new(TimeLength - other.TimeLength);
        public static TimePeriod Minus(TimePeriod t1, TimePeriod t2) => new(Math.Abs(t1.TimeLength - t2.TimeLength));

        public static TimePeriod operator +(TimePeriod t1, TimePeriod t2) => t1.Plus(t2);
        public static TimePeriod operator -(TimePeriod t1, TimePeriod t2) => t1.Minus(t2);
        public static TimePeriod operator *(int x, TimePeriod t) => new(x * t.TimeLength);
        public static TimePeriod operator /(TimePeriod t, int x) => new(t.TimeLength / x);
        public static TimePeriod operator ++(TimePeriod t) => new(t.TimeLength + 1);
        public static TimePeriod operator --(TimePeriod t) => new(t.TimeLength - 1);

    }
}

