using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilteringSystem.BusinessLayer;

namespace NapierBankMessageFilteringSystemUnitTests
{
    [TestClass]
    public class EmailTest
    {
        protected Email email = new Email();
        [TestMethod]
        public void TestEmailSender() // Unit Test to test the E-mail Sender
        {
            email.EmailSender = "sabinlungu293@gmail.com";
            string expectedValue = "sabinlungu293@gmail.com";

            Assert.AreEqual(expectedValue, expectedValue);
        }

        [TestMethod]
        public void TestEmailSenderTwo()
        {
            email.EmailSender = "mike23@yahoo.com";
            string expectedValue = "mike23@yahoo.com";

            Assert.AreEqual(expectedValue, email.EmailSender);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEmailSenderFail()
        {
            email.EmailSender = " ";
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEmailSenderFailTwo()
        {
            email.EmailSender = "bob23";
        }
    }
}