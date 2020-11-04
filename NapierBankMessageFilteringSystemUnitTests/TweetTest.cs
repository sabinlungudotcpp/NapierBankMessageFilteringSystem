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
            tweets.TweetSender = "@DawsonJ";
            string expectedValue = "@DawsonJ";

            Assert.AreEqual(expectedValue, tweets.TweetSender);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTweetSenderFail() // Unit Test to determine if it will pass in the circumstance that the tweet sender is empty
        {
            tweets.TweetSender = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTweetSenderFailTwo()
        {
            tweets.TweetSender = "Jamie Brown";
        }

        [TestMethod]
        public void TweetText() // Unit Test for the Tweet Text
        {
            tweets.TweetText = "I found this OMG";
            string expectedValue = "I found this OMG";

            Assert.AreEqual(expectedValue, tweets.TweetText);
        }

        [TestMethod]
        public void TweetTextTwo()
        {
            tweets.TweetText = "Check this radio station out #CapitalFM";
            string expectedValue = "Check this radio station out #CapitalFM";

            Assert.AreEqual(expectedValue, tweets.TweetText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTweetFail()
        {
            tweets.TweetText = "";
        }
    }
}
