﻿using System;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    public class Email : Message
    {
        private string emailSender; // The E-mail Sender
        private string subject; // The E-mail Subject
        private string emailText; // E-mail Text
        private int defaultVal = 0;

        public Email() // Default e-mail constructor
        {

        }
        public string EmailSender
        {
            set
            {
                if(value.Length > defaultVal) {
                    
                   var email_address = new System.Net.Mail.MailAddress(value);
                   this.emailSender = value; // Set the E-mail sender to its value
                 }

                else
                {
                    throw new FormatException("E-mail address is not in the correct format. Please re-enter");
                }
            }
          
           get
            {
                return this.emailSender;
            }
        }

        public string Subject
        {
            set
            {

              if(value.Length > defaultVal && value.Length <= 20) {
               
                this.subject = value;

               }

                else {
                   
                 throw new ArgumentException("Subject exceeds 20 characters. Please re-enter!");

                    }
                }

            get {
            
              return this.subject;
            }
        }

        public string EmailText
        {
            set
            {

                if(value.Length >= defaultVal && value.Length <= 1028) {  // If the e-mail text is between 0 and 1028
                    
                     this.emailText = value;
                   }

                  else { 
                    
                   throw new ArgumentException("E-mail text is not valid. Please re-enter");

                    }
                } 

            get {
            
             return this.emailText; // Return the e-mail text property

            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(Object emailObject)
        {
            return this.Equals(emailObject as Email);
           
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}