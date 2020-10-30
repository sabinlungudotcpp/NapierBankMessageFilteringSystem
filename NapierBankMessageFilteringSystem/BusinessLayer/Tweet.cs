using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 21/10/2020
    public class Tweet : Message // Tweet class inherits from Messages class
    {
        private string tweetSender;
        private string tweetText;
        private string atSymbol = "@";

        public string TweetSender
        {
            set
            {
                try
                {
                   if(isTweetSenderValid())
                    {
                        this.tweetSender = value;
                    }

                   else if(!isTweetSenderValid())
                    {
                        throw new Exception("Tweet sender does not start with an @ symbol. Re-enter please");
                    }
                } 
                
                catch
                {
                    throw new ArgumentException("Tweet Sender does not start with an @. Please re-enter");
                }
            }

            get
            {
                return this.tweetSender;
            }
        }

        public string TweetText
        {
            set
            {
                try
                {
                    if(isTweetTextValid())
                    {
                        this.tweetText = value;
                    }

                    else if(!isTweetTextValid())
                    {
                        throw new ArgumentException("Tweet text is not valid. Please re-enter");
                    }
                } 
                
                catch
                {
                    throw new Exception("Tweet Text must be a maximum of 140 characters long. Please re-enter");
                }
            }

            get
            {
                return this.tweetText;
            }
        }

        private bool isTweetSenderValid() // Determines if the Tweet sender is valid or not
        {
            return this.tweetSender.StartsWith(atSymbol) && this.tweetSender.Length > 0 && this.tweetSender.Length <= 16;
        }

        private bool isTweetTextValid()
        {
            return this.tweetText.Length > 0 && this.tweetText.Length <= 140;
        } 
    }
}
