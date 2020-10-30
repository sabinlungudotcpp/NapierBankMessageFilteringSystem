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
                if(value.StartsWith(atSymbol) && value.Length > 0 && value.Length <= 16) { 
                   
                   this.tweetSender = value;

                }

                   else {
                    
                    throw new Exception("Tweet sender does not start with an @ symbol. Re-enter please");
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
                if(value.Length > 0 && value.Length <= 140) { 
                    
                        this.tweetText = value;
                    }

                    else 
                    {
                        throw new ArgumentException("Tweet text is not valid. Please re-enter");
                 }
            }

            get
            {
                return this.tweetText;
            }
        }
    }
}