using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 21/10/2020
    public class Message // Message Class.
    {
        public string messageID;
        public string messageBody;

        public string MessageID
        {
            set
            {
                try
                {
                    if (isIdValid())
                    {
                        this.messageID = value.ToUpper(); // Set the message ID to uppercase if it's valid
                    }
                } 
                
                catch
                {
                    throw new ArgumentException("Message ID is invalid. It should begin with S, E or T and it should be a maximum of 10 characters.");
                }
            }

            get
            {
                return messageID;
            }

        }

        public string MessageBody
        {
            set
            {
                if(isBodyValid())
                {
                    this.messageBody = value;
                }

                else
                {
                    throw new ArgumentException("The body text must be > 0 characters.");
                }
            }

            get
            {
                return this.messageBody;
            }
        }

        public bool isIdValid() // Determines if the Message ID is valid or not
        {
            return messageID.Length == 10 && int.TryParse(messageID.Remove(0, 1), out int i); // Returns true or false if the length of the ID is 10 characters and the rest 9 are numbers and letters
        }

        public bool isBodyValid() // Determines if the body of the message is valid or not
        {
            return messageBody.Length > 0;
        }
    }
}
