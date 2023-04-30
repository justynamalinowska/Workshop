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
    public class UnitTestsPudelkoConstructors
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
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0)]
        [DataRow((byte)15, (byte)30, (byte)15, (byte)30)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        public void Constructor_2params_noException(byte h, byte m, byte expectedH, byte expectedM)
        {
            Time t = new Time(h, m);

            AssertTime(t, expectedH, expectedM, 0);
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
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        [DataRow("00:00:00", (byte)0, (byte)0, (byte)0)]
        [DataRow("02:30:15", (byte)2, (byte)30, (byte)15)]
        [DataRow("10:20:05", (byte)10, (byte)20, (byte)5)]
        public void Constructor_stringGiven_noException(string s, byte expectedH, byte expectedM, byte expectedS)
        {
            Time t = new Time(s);

            AssertTime(t, expectedH, expectedM, expectedS);
        }



        #endregion
    }
}






       
    
