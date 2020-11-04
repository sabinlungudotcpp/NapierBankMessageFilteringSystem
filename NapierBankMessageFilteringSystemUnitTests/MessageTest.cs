using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilteringSystem.BusinessLayer;

namespace NapierBankMessageFilteringSystemUnitTests
{
    [TestClass]
    public class MessageTest
    {
        Message message = new Message();

        [TestMethod]
        public void MessageIDPass()
        {
            message.MessageID = "s123456789";
            string expectedID = "S123456789";

            Assert.AreEqual(expectedID, message.MessageID); // Determines if the actual and expected values are the same.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageIDFail() // Unit Test for testing an invalid Message ID
        {
           message.MessageID = "e00000000000";
        }

        [TestMethod]
        public void MessageBodyPass()
        {
            message.MessageBody = "Hello Sabin Lungu";
            string expectedBodyMsg = "Hello Sabin Lungu";
            Assert.AreEqual(expectedBodyMsg, message.MessageBody);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageBodyFail()
        { 
            message.MessageBody = "";
        }
    }
}
