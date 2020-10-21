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
        private string messageID;
        private string messageBody;

        public string MessageID
        {
            set
            {
               if(isIdValid())
                {
                    this.messageID = value.ToUpper(); // Set the message ID to uppercase if it's valid
                }

                else
                {
                    throw new ArgumentException("Message ID is invalid. It must start with S, E or T");
                }
            }

            get
            {
                return this.messageID;
            }
        }

        public string MessageBody
        {
            set
            {

            }

            get
            {
                return this.messageBody;
            }
        }

        public bool isIdValid() // Determines if the Message ID is valid or not
        {
            return messageID.Length == 10 && int.TryParse(messageID.Remove(0, 1), out _);
        }


    }
}
