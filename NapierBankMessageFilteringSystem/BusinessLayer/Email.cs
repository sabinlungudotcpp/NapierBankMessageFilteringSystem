using System;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    public class Email : Message
    {
        private string emailSender; // The E-mail Sender
        private string subject; // The E-mail Subject
        private string emailText; // E-mail Text

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
                try
                {
                    if(isSubjectValid())
                    {
                        this.subject = value;
                    }

                    else if(!isSubjectValid())
                    {
                        throw new ArgumentException("Subject exceeds 20 characters. Please re-enter!");
                    }
                }

                catch
                {
                    throw new Exception("Subject exceeds 20 characters. Please re-enter");
                }
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
                try
                {
                    if(isEmailTxtValid())
                    {
                        this.emailText = value;
                    }

                    else if(!isEmailTxtValid())
                    {
                        throw new ArgumentException("E-mail text is not valid. Please re-enter");
                    }
                } 
                
                catch
                {
                    throw new Exception("E-mail Text must be a maximum of 1028 characters. Please re-enter");
                }
            }

            get
            {
                return this.emailText;
            }
        }

        private bool isEmailSenderValid() // Determines if the e-mail sender is valid or not
        {
            return this.emailSender.Length > 0 && this.emailSender != null; // Returns true or false if the length is > 0 and the e-mail sender field is not left empty
        }

        private bool isSubjectValid()
        {
            return this.subject.Length > 0 && this.subject.Length <= 20 && this.subject != null;
        }

        private bool isEmailTxtValid() // Determines if the e-mail text is valid or not
        {
            return this.emailText.Length >= 0 && this.emailText.Length <= 1028;
        }
    }
}
