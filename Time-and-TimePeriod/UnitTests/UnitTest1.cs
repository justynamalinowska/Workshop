using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeAndTimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace UnitTests
{
    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    #region Constructor tests ================================
    [TestClass]
    public class UnitTestsTimeConstructors
    {
        private static byte defaultValue = 0;

        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Time t = new Time();

            Assert.AreEqual(defaultValue, t.Hours);
            Assert.AreEqual(defaultValue, t.Minutes);
            Assert.AreEqual(defaultValue, t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)15, (byte)30, (byte)20, (byte)15, (byte)30, (byte)20)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void Constructor_3params_noException(byte h, byte m, byte s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(h, m, s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)1, (byte)60, (byte)25)]
        [DataRow((byte)24, (byte)30, (byte)04)]
        [DataRow((byte)11, (byte)1, (byte)60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_ArgumentOutOfRangeException(byte h, byte m, byte s)
        {
            Time p = new Time(h, m, s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)15, (byte)30, (byte)15, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        public void Constructor_2params_noException(byte h, byte m, byte expectedH, byte expectedM)
        {
            Time t = new Time(h, m);

            AssertTime(t, expectedH, expectedM, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)2, (byte)60)]
        [DataRow((byte)24, (byte)30)]
        [DataRow((byte)60, (byte)1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_ArgumentOutOfRangeException(byte h, byte m)
        {
            Time p = new Time(h, m);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)0, (byte)0)]
        [DataRow((byte)15, (byte)15)]
        [DataRow((byte)23, (byte)23)]
        public void Constructor_1param_noException(byte h, byte expectedH)
        {
            Time t = new Time(h);

            AssertTime(t, expectedH, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)25)]
        [DataRow((byte)60)]
        [DataRow((byte)90)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_ArgumentOutOfRangeException(byte h)
        {
            Time p = new Time(h);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(86399, (byte)23, (byte)59, (byte)59)]
        [DataRow(0, (byte)0, (byte)0, (byte)0)]
        [DataRow(9015, (byte)2, (byte)30, (byte)15)]
        [DataRow(37205, (byte)10, (byte)20, (byte)5)]
        public void Constructor_onlySecondsGiven_noException(long s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(86400)]
        [DataRow(-10)]
        [DataRow(100000)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_onlySecondsGiven_ArgumentOutOfRangeException(long s)
        {
            Time p = new Time(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("02:30:15", (byte)2, (byte)30, (byte)15)]
        [DataRow("10:20:05", (byte)10, (byte)20, (byte)5)]
        public void Constructor_stringGiven_noException(string s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:00:00")]
        [DataRow("00:60:00")]
        [DataRow("02:30:60")]
        [DataRow("25:20:05")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_stringGiven_ArgumentOutOfRangeException(string s)
        {
            Time p = new Time(s);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:00")]
        [DataRow("00:40:0")]
        [DataRow("2:30:60")]
        [DataRow("25:2:5")]
        [ExpectedException(typeof(Exception))]
        public void Constructor_stringGiven_Exception(string s)
        {
            Time p = new Time(s);
        }
    }

    [TestClass]
    public class UnitTestsTimePeriodConstructors
        {
            private static byte defaultValue = 0;

            private void AssertTimePeriod(Time t, byte expectedt)
            {
                Assert.AreEqual(expectedt, t);
            }

            [TestMethod, TestCategory("Constructors")]
            public void Constructor_Default()
            {
                TimePeriod t = new TimePeriod();

                Assert.AreEqual(defaultValue, t.TimeLength);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(0, (byte)0, (byte)0, (byte)0)]
            [DataRow(55820, (byte)15, (byte)30, (byte)20)]
            [DataRow(86399, (byte)23, (byte)59, (byte)59)]
            public void Constructor_SecondsGiven_noException(long time, byte expectedH, byte expectedM, byte expectedS)
            {
                TimePeriod t = new TimePeriod(time);

                Assert.AreEqual(t.TimeLength, time);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-20)]
            [DataRow(-4656)]
            [DataRow(-1)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_SecondsGiven_Exception(long time)
            {
                TimePeriod t = new TimePeriod(time);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow((byte)0, (byte)0, (byte)0, 0)]
            [DataRow((byte)45, (byte)30, (byte)20, 163820)]
            [DataRow((byte)23, (byte)59, (byte)59, 86399)]
            public void Constructor_3params_noException(byte h, byte m, byte s, long expectedSeconds)
            {
                TimePeriod t = new TimePeriod(h, m, s);

                Assert.AreEqual(t.TimeLength, expectedSeconds);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow((byte)0, (byte)60, (byte)0)]
            [DataRow((byte)15, (byte)30, (byte)70)]
            [DataRow((byte)5, (byte)69, (byte)59)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_3params_Exception(byte h, byte m, byte s)
            {
                TimePeriod t = new TimePeriod(h, m, s);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow((byte)0, (byte)0, 0)]
            [DataRow((byte)45, (byte)30, 163800)]
            [DataRow((byte)23, (byte)59, 86340)]
            public void Constructor_2params_noException(byte h, byte m, long expectedSeconds)
            {
                TimePeriod t = new TimePeriod(h, m);

                Assert.AreEqual(t.TimeLength, expectedSeconds);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow((byte)0, (byte)60)]
            [DataRow((byte)15, (byte)90)]
            [DataRow((byte)5, (byte)69)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_2params_Exception(byte h, byte m)
            {
                TimePeriod t = new TimePeriod(h, m);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow((byte)0, 0)]
            [DataRow((byte)45, 162000)]
            [DataRow((byte)23, 82800)]
            public void Constructor_1params_noException(byte h, long expectedSeconds)
            {
                TimePeriod t = new TimePeriod(h);

                Assert.AreEqual(t.TimeLength, expectedSeconds);
            }

            [DataTestMethod, TestCategory("Constructors")]
            public void Constructor_2TimeParams_noException()
            {
                // Arrange
                var t1 = new Time(5, 30, 0);
                var t2 = new Time(8, 45, 10);

                // Act
                var timePeriod = new TimePeriod(t1, t2);

                // Assert
                Assert.AreEqual(3, timePeriod.TimeLength / 3600);
                Assert.AreEqual(15, (timePeriod.TimeLength / 60) % 60);
                Assert.AreEqual(10, timePeriod.TimeLength % 60);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow("23:59:59", 86399)]
            [DataRow("00:00:00", 0)]
            [DataRow("2:30:15", 9015)]
            [DataRow("10:20:05", 37205)]
            public void Constructor_stringGiven_noException(string s, long expectedSeconds)
            {
                TimePeriod t = new TimePeriod(s);

                Assert.AreEqual(t.TimeLength, expectedSeconds);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow("22:00:80")]
            [DataRow("00:60:00")]
            [DataRow("02:30:60")]
            [DataRow("9:20:95")]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_stringGiven_ArgumentOutOfRangeException(string s)
            {
                TimePeriod t = new TimePeriod(s);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow("23:00")]
            [DataRow("0:40:0")]
            [DataRow("2:3:0")]
            [DataRow("25:2:5")]
            [ExpectedException(typeof(Exception))]
            public void Constructor_stringGiven_Exception(string s)
            {
                TimePeriod t = new TimePeriod(s);
            }
        }

    #endregion
    #region ToString tests ===================================
    [TestClass]
    public class UnitTestsTimeToString
    {
        [DataTestMethod, TestCategory("String representation")]
        [DataRow((byte)23, (byte)59, (byte)59, "23:59:59")]
        [DataRow((byte)0, (byte)0, (byte)0, "00:00:00")]
        [DataRow((byte)2, (byte)30, (byte)15, "02:30:15")]
        [DataRow((byte)10, (byte)20, (byte)5, "10:20:05")]
        public void Time_ToString_Formattable(byte h, byte m, byte s, string expectedStringRepresentation)
        {
            var time = new Time(h, m, s);
            Assert.AreEqual(expectedStringRepresentation, time.ToString());
        }
    }

    public class UnitTestsTimePeriodToString
    {
        [DataTestMethod, TestCategory("String representation")]
        [DataRow(86399, "23:59:59")]
        [DataRow(0, "00:00:00")]
        [DataRow(9015, "02:30:15")]
        [DataRow(37205, "10:20:05")]
        public void TimePeriod_ToString_Formattable(long seconds, string expectedStringRepresentation)
        {
            var time = new TimePeriod(seconds);
            Assert.AreEqual(expectedStringRepresentation, time.ToString());
        }
    }
    #endregion
    
    #region Equals tests ===================================
    [TestClass]
    public class UnitTestsTimeEquals
    {
        public void UnitTests_EqualsWithEqualTimes()
        {
            var p1 = new Time(22, 4, 39);
            var p2 = new Time(22, 04, 39);

            Assert.AreEqual(p1, p2);
        }


        public void UnitTests_EqualsWithDifferentTimes()
        {
            var p1 = new Time(11, 36, 59);
            var p2 = new Time(11, 36, 56);

            bool areEqual = p1.Equals(p2);

            Assert.IsFalse(areEqual);
        }
    }
    public class UnitTestsTimePeriodEquals
    {
        public void UnitTests_EqualsWithEqualTimePeriods()
        {
            var p1 = new TimePeriod(64987);
            var p2 = new TimePeriod(64987);

            Assert.AreEqual(p1, p2);
        }


        public void UnitTests_EqualsWithDifferentTimePeriods()
        {
            var p1 = new TimePeriod(5326);
            var p2 = new TimePeriod(5329);

            bool areEqual = p1.Equals(p2);

            Assert.IsFalse(areEqual);
        }
    }
    #endregion
}








