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


    [TestClass]
    public class UnitTestsTimeConstructors
    {
        private static byte defaultValue = 0;

        #region Time Constructor tests ================================

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
        public void Constructor_3params_noException(byte h, byte m, byte s, byte expectedH, byte  expectedM, byte expectedS)
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
        #endregion
    }
}






       
    
