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
                if (value.Length == 10)
                    {
                        this.messageID = value.ToUpper(); // Set the message ID to uppercase if it's valid
                    }

                else
                {
                    throw new ArgumentException("Message ID invalid");
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
  
              if(!(value.Length <= 0))
                    {
                        this.messageBody = value;
                    }

              else
                {
                    throw new ArgumentException("Message Body invalid");
               }

                }  
            
            get
            {
                return this.messageBody;
            }
        }
    }
}
