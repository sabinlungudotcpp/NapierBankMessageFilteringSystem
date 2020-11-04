using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.BusinessLayer
{
    public class Sms : Message // SMS class inherits feeatures from the Message base class.
    {
        private string regexMatcher = "[0-9]{1,10}";
        private string sender; // The SMS Sender
        private string countryCode; // The SMS country code
        private string smsText; // The SMS text
        private string code = "+";
        private int defaultValue = 0;

        public Sms()
        {

        }
        public string Sender
        {
            set
            {
                if(Regex.IsMatch(value, regexMatcher))
                {
                    this.sender = value;
                }

                else
                {
                    throw new Exception("SMS Sender invalid");
                }
            }

            get
            {
                return this.sender;
            }
        }

        public string CountryCode
        {
            set
            {
                if(value.StartsWith(code))
                {
                    this.countryCode = value;
                }

                else
                {
                    throw new ArgumentException("SMS country code must start with +");
                }
            }

            get
            {
                return this.countryCode;
            }
        }

        public string SmsText
        {
            set
            {
                if(value.Length > defaultValue && value.Length <= 140)
                {
                    this.smsText = value;
                }

                else
                {
                    throw new ArgumentException("The SMS text must have more than 0 characters and a maximum of 140");
                }
            }

            get
            {
                return this.smsText; // Return the SMS text
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}