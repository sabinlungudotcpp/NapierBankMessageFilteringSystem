﻿using System;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 21/10/2020
    // Purpose of Class: Tweet class used to store the tweet sender ID, tweet text and the symbol for the sender.
    // Any Errors: N/A

    public class Tweet : Message // Tweet class inherits from Messages class
    {
        private string tweetSender;
        private string tweetText;
        private string atSymbol = "@";

        public Tweet() // Tweet default constructor
        {

        }
        public string TweetSender
        {
            set
            {
                if(value.StartsWith(atSymbol) && value.Length >= 0 && value.Length <= 16) { 
                   
                   this.tweetSender = value;
                }

                else {
                    
                 throw new ArgumentException("Tweet sender does not start with an @ symbol. Re-enter please");
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
                if(value.Length > 0 && value.Length <= 140) { // If the tweet length is > 0 and <= 140 words
                   
                      this.tweetText = value; // Set the tweet text to its value
                    }

                    else 
                    {
                        throw new ArgumentException("Tweet text is not valid. Please re-enter");
                 }
            }

            get
            {
                return this.tweetText; // Returns the tweet text
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(Object tweetObject)
        {
            return this.Equals(tweetObject as Tweet);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}