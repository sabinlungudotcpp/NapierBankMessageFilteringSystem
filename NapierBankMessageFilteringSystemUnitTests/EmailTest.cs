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
        public void TestEmailSenderNew()
        {
            email.EmailSender = "bob90@gmail.com";
            string expectedValue = "bob90@gmail.com";

            Assert.AreEqual(expectedValue, email.EmailSender);
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

        [TestMethod]
        public void TestEmailSubject() // Unit Test method to check if the e-mail subject is valid or not
        {
            email.Subject = "Incident Report";
            string expectedValue = "Incident Report";

            Assert.AreEqual(expectedValue, email.Subject);
        }

        [TestMethod]
        public void TestEmailSubjectTwo()
        {
            email.Subject = "Theft Report";
            string expectedValue = "Theft Report";

            Assert.AreEqual(expectedValue, email.Subject);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptySubject()
        {
            email.Subject = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidSubject()
        {
            email.Subject = "Theft happened 20 minutes ago and we need to take action as soon as possible otherwise they will steal all of our money, FAST";

        }

        [TestMethod]
        public void TestEmailText()
        {
            email.EmailText = "Theft happened 10 minutes ago";
            string expectedValue = "Theft happened 10 minutes ago";

            Assert.AreEqual(expectedValue, email.EmailText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestInvalidEmailText()
        {
            email.EmailText = "In the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the courseworkIn the coursework I have tested empty inputs for example the Tweet Sender and they pass, I assume it's valid to test invalid test cases in the coursework";

        }
    }
}