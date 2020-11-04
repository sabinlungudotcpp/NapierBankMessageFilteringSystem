using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NapierBankMessageFilteringSystem.BusinessLayer;

namespace NapierBankMessageFilteringSystemUnitTests
{
    [TestClass]
    public class TweetTest
    {
        Tweet tweets = new Tweet();
        [TestMethod]
        public void TestTweetSender()
        {

            tweets.TweetSender = "@SabinLungu";
            string expectedValue = "@SabinLungu";

            Assert.AreEqual(expectedValue, tweets.TweetSender);
        }

        [TestMethod]
        public void TestTweetSenderTwo()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTweetSenderFail()
        {

        }
    }
}
