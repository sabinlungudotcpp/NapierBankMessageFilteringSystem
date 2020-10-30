using System;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    public class Email : Message
    {
        private string emailSender; // The E-mail Sender
        private string subject; // The E-mail Subject
        private string emailText; // E-mail Text

        public Email()
        {

        }

        public string EmailSender
        {
            set
            {
                if(value.Length > 0) {
                    
                   var email_address = new System.Net.Mail.MailAddress(value);
                   this.emailSender = value; // Set the E-mail sender to its value
                 }

                else
                {
                    throw new Exception("E-mail address is not in the correct format. Please re-enter");
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

              if(value.Length > 0 && value.Length <= 20) {
               
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

                if(value.Length >= 0 && value.Length <= 1028) { 
                    
                     this.emailText = value;
                   }

                  else { 
                    
                   throw new ArgumentException("E-mail text is not valid. Please re-enter");
                    }
                } 

            get {
            
             return this.emailText;
            }
        }
    }
}