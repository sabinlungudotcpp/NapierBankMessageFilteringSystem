using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilteringSystem.BusinessLayer;

namespace NapierBankMessageFilteringSystemUnitTests
{
    [TestClass]
    public class SmsTest
    {
        Sms sms = new Sms();

        [TestMethod]
        public void SMSTestSender()
        {
            sms.Sender = "78 81365475";
            string expectedSmsSender = "78 81365475";

            Assert.AreEqual(expectedSmsSender, sms.Sender);
        }

        [TestMethod]
        public void CountryCodeTest()
        {
            sms.CountryCode = "+44";
            string expectedCode = "+44";

            Assert.AreEqual(expectedCode, sms.CountryCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CountryCodeTestFail()
        {
            sms.CountryCode = "49";
        }
    }
}
