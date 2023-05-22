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

            Assert.AreEqual(defaultValue, t.Hours * 3600 + t.Minutes * 60 + t.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, (byte)0, (byte)0, (byte)0)]
        [DataRow(55820, (byte)15, (byte)30, (byte)20)]
        [DataRow(86399, (byte)23, (byte)59, (byte)59)]
        public void Constructor_SecondsGiven_noException(long time, byte expectedH, byte expectedM, byte expectedS)
        {
            TimePeriod t = new TimePeriod(time);

            Assert.AreEqual(t.Hours * 3600 + t.Minutes * 60 + t.Seconds, time);
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

            Assert.AreEqual(t.Hours * 3600 + t.Minutes * 60 + t.Seconds, expectedSeconds);
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

            Assert.AreEqual(t.Hours * 3600 + t.Minutes * 60 + t.Seconds, expectedSeconds);
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

            Assert.AreEqual(t.Hours * 3600 + t.Minutes * 60 + t.Seconds, expectedSeconds);
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
            Assert.AreEqual(3, timePeriod.Hours);
            Assert.AreEqual(15, timePeriod.Minutes);
            Assert.AreEqual(10, timePeriod.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:59:59", 86399)]
        [DataRow("00:00:00", 0)]
        [DataRow("2:30:15", 9015)]
        [DataRow("10:20:05", 37205)]
        public void Constructor_stringGiven_noException(string s, long expectedSeconds)
        {
            TimePeriod t = new TimePeriod(s);

            Assert.AreEqual(t.Hours * 3600 + t.Minutes * 60 + t.Seconds, expectedSeconds);
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
    [TestClass]
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

        public void UnitTests_EqualsOperatorWithEqualTimes()
        {
            var p1 = new Time(23, 4, 39);
            var p2 = new Time(23, 04, 39);
            bool areEqual = p1==p2;
            Assert.IsTrue(areEqual);
        }

        public void UnitTests_EqualsOperatorsWithDifferentTimes()
        {
            var p1 = new Time(11, 36, 59);
            var p2 = new Time(11, 32, 56);

            bool areEqual = p1!=p2;

            Assert.IsTrue(areEqual);
        }
    }
    [TestClass]
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
        public void UnitTests_EqualsOperatorsWithEqualTimePeriods()
        {
            var p1 = new TimePeriod(94987);
            var p2 = new TimePeriod(94987);
            bool areEqual = p1 == p2;
            Assert.IsTrue(true);
        }


        public void UnitTests_EqualsOperatorsWithDifferentTimePeriods()
        {
            var p1 = new TimePeriod(5326);
            var p2 = new TimePeriod(5329);

            bool areEqual = p1!=p2;

            Assert.IsTrue(areEqual);
        }
    }
    #endregion
    #region operators tests ===================================
    [TestClass]
    public class UnitTestsTimeOperators
    {
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_PlusSign_TwoTimes()
        {
            var t1 = new Time(10, 20, 30);
            var t2 = new Time(4, 50, 0);

            var expectedTime = new Time(15, 10, 30);

            Time actualTime = t1 + t2;

            Assert.AreEqual(expectedTime, actualTime);
        }
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_PlusSign_TimeAndTimePeriod()
        {
            var t1 = new Time(10, 20, 30);
            var t2 = new TimePeriod(36000);

            var expectedTime = new Time(20, 20, 30);

            Time actualTime = t1 + t2;

            Assert.AreEqual(expectedTime, actualTime);
        }
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_Increment()
        {
            var t1 = new Time(10, 20, 30);

            var expectedTime = new Time(10, 20, 31);

            Assert.AreEqual(expectedTime.Hours, t1.Hours);
            Assert.AreEqual(expectedTime.Minutes, t1.Minutes);
            Assert.AreEqual(expectedTime.Seconds, t1.Seconds + 1);
        }
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_Decrement()
        {
            var t1 = new Time(10, 20, 30);

            var expectedTime = new Time(10, 20, 29);

            Assert.AreEqual(expectedTime.Hours, t1.Hours);
            Assert.AreEqual(expectedTime.Minutes, t1.Minutes);
            Assert.AreEqual(expectedTime.Seconds, t1.Seconds - 1);
        }
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)10, (byte)20, (byte)30, 2, (byte)5, (byte)10, (byte)15)]
        [DataRow((byte)4, (byte)50, (byte)0, 2, (byte)2, (byte)25, (byte)0)]
        [DataRow((byte)9, (byte)12, (byte)15, 3, (byte)3, (byte)4, (byte)5)]
        public void UnitTests_overloading_Division(byte h, byte m, byte s, int x, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            Time actualTime = t1 / x;
            var expectedTime = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(actualTime, expectedTime);
        }
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)10, (byte)20, (byte)30, 2, (byte)20, (byte)41, (byte)0)]
        [DataRow((byte)4, (byte)50, (byte)0, 2, (byte)9, (byte)40, (byte)0)]
        [DataRow((byte)3, (byte)12, (byte)15, 3, (byte)9, (byte)36, (byte)45)]
        public void UnitTests_overloading_Multiplication(byte h, byte m, byte s, int x, byte expectedH, byte expectedM, byte expectedS)
        {
            var t1 = new Time(h, m, s);
            Time actualTime = x * t1;
            var expectedTime = new Time(expectedH, expectedM, expectedS);

            Assert.AreEqual(actualTime, expectedTime);
        }

    }

    [TestClass]
    public class UnitTestsTimePeriodOperators
    {
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_PlusSign_TwoTimePeriods()
        {
            var t1 = new TimePeriod(13723);
            var t2 = new TimePeriod(1500);

            var expectedTime = new TimePeriod(15223);

            TimePeriod actualTime = t1 + t2;

            Assert.AreEqual(expectedTime, actualTime);
        }
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_Increment()
        {
            var t1 = new TimePeriod(13723);

            var expectedTime = new TimePeriod(13724);

            Assert.AreEqual(expectedTime.Hours * 3600 + expectedTime.Minutes * 60 + expectedTime.Seconds, t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds + 1);
        }
        [TestMethod, TestCategory("Operators")]
        public void UnitTests_overloading_Decrement()
        {
            var t1 = new TimePeriod(13723);

            var expectedTime = new TimePeriod(13722);

            Assert.AreEqual(expectedTime.Hours * 3600 + expectedTime.Minutes * 60 + expectedTime.Seconds, t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds - 1);
        }
        [TestMethod, TestCategory("Operators")]
        [DataRow(5000, 2, 10000)]
        [DataRow(11111, 3, 33333)]
        [DataRow(22222, 4, 88888)]
        public void UnitTests_overloading_Multiplication(long seconds, int x, long expectedSeconds)
        {
            var t1 = new TimePeriod(seconds);
            TimePeriod actualTime = x * t1;
            var expectedTime = new TimePeriod(expectedSeconds);

            Assert.AreEqual(actualTime, expectedTime);
        }
        [TestMethod, TestCategory("Operators")]
        [DataRow(5000, 2, 2500)]
        [DataRow(33333, 3, 11111)]
        [DataRow(88888, 4, 22222)]
        public void UnitTests_overloading_Division(long seconds, int x, long expectedSeconds)
        {
            var t1 = new TimePeriod(seconds);
            TimePeriod actualTime = t1 / x;
            var expectedTime = new TimePeriod(expectedSeconds);

            Assert.AreEqual(actualTime, expectedTime);
        }
    }
    #endregion
    #region Comparsion tests
    [TestClass]
    public class UnitTestsTimeComparsionOperators
    {
            [TestMethod, TestCategory("ComparsionOperators")]
            [DataTestMethod]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)1, (byte)0)]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)1, (byte)0, (byte)0)]
            public void LessThan_Operator_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
            {
                var t1 = new Time(h1, m1, s1);
                var t2 = new Time(h2, m2, s2);

                Assert.IsTrue(t1 < t2);
            }
            [TestMethod, TestCategory("ComparsionOperators")]
            [DataTestMethod]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)1)]
            [DataRow((byte)0, (byte)0, (byte)1, (byte)0, (byte)1, (byte)0)]
            public void LessThanOrEqual_Operator_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
            {
                var t1 = new Time(h1, m1, s1);
                var t2 = new Time(h2, m2, s2);

                Assert.IsTrue(t1 <= t2);
            }
            [TestMethod, TestCategory("ComparsionOperators")]
            [DataTestMethod]
            [DataRow((byte)0, (byte)0, (byte)1, (byte)0, (byte)0, (byte)0)]
            [DataRow((byte)0, (byte)1, (byte)0, (byte)0, (byte)0, (byte)0)]
            [DataRow((byte)1, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
            public void GreaterThan_Operator_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
            {
                var t1 = new Time(h1, m1, s1);
                var t2 = new Time(h2, m2, s2);

                Assert.IsTrue(t1 > t2);
            }
            [TestMethod, TestCategory("ComparsionOperators")]
            [DataTestMethod]
            [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0)]
            [DataRow((byte)0, (byte)0, (byte)1, (byte)0, (byte)0, (byte)0)]
            [DataRow((byte)0, (byte)1, (byte)0, (byte)0, (byte)0, (byte)0)]
            public void GreaterThanOrEqual_Operator_ReturnsTrue(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
            {
                var t1 = new Time(h1, m1, s1);
                var t2 = new Time(h2, m2, s2);

                Assert.IsTrue(t1 >= t2);
            }

    }
    [TestClass]
    public class UnitTestsTimePeriodComparsionOperators
    {
        [TestMethod, TestCategory("ComparsionOperators")]
        [DataTestMethod]
        [DataRow(677, 838)]
        [DataRow(3124, 10000)]
        [DataRow(4232, 4233)]
        public void LessThan_Operator_ReturnsTrue(long s1, long s2)
        {
            var t1 = new TimePeriod(s1);
            var t2 = new TimePeriod(s2);

            Assert.IsTrue(t1 < t2);
        }
        [TestMethod, TestCategory("ComparsionOperators")]
        [DataTestMethod]
        [DataRow(734, 853)]
        [DataRow(10000, 10000)]
        [DataRow(4233, 4233)]
        public void LessThanOrEqual_Operator_ReturnsTrue(long s1, long s2)
        {
            var t1 = new TimePeriod(s1);
            var t2 = new TimePeriod(s2);

            Assert.IsTrue(t1 <= t2);
        }
        [TestMethod, TestCategory("ComparsionOperators")]
        [DataTestMethod]
        [DataRow(853,300)]
        [DataRow(10030, 10000)]
        [DataRow(4234, 4233)]
        public void GreaterThan_Operator_ReturnsTrue(long s1, long s2)
        {
            var t1 = new TimePeriod(s1);
            var t2 = new TimePeriod(s2);

            Assert.IsTrue(t1 > t2);
        }
        [TestMethod, TestCategory("ComparsionOperators")]
        [DataTestMethod]
        [DataRow(730, 436)]
        [DataRow(40000, 40000)]
        [DataRow(767, 767)]
        public void GreaterThanOrEqual_Operator_ReturnsTrue(long s1, long s2)
        {
            var t1 = new TimePeriod(s1);
            var t2 = new TimePeriod(s2);

            Assert.IsTrue(t1 >= t2);
        }
    }
    #endregion
}







