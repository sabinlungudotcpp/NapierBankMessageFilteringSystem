﻿using System;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 21/10/2020
    public class Message // Message Class.
    {
        private string messageID; // The Message ID that is entered by the user
        private string messageBody; // The message body that is entered by the user
        public Message()
        {

        }
        public string MessageID
        {
            set
            {
                if (value.Length == 10) // If the length of the message id is 10
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

        public static implicit operator string(Message v)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
