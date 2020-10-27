using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    public class Email : Message
    {
        private string emailSender; // The E-mail Sender
        private string subject;
        private string emailText;

        public Email() // Default constructor
        {

        }

        public string EmailSender
        {
            set
            {
                try
                {
                    if(isEmailSenderValid())
                    {
                        var email_address = new System.Net.Mail.MailAddress(value);
                        this.emailSender = value; // Set the E-mail sender to its value
                    }
                }

                catch
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

            }

            get
            {
                return this.subject;
            }
        }

        public string EmailText
        {
            set
            {
                
            }

            get
            {
                return this.emailText;
            }
        }

        private bool isEmailSenderValid()
        {
            return this.emailSender.Length > 0 && this.emailSender != null; // Returns true or false if the length is > 0 and the e-mail sender field is not left empty
        }


    }
}
