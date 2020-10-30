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
        private string regexMatcher = "^([/+]?[0-9]{1,3}[/s.-][0-9]{1,12})([/s.-]?[0-9]{1,4}?)$";
        private string sender; // The SMS Sender
        private string countryCode; // The SMS country code
        private string smsText; // The SMS text
        private string code = "+";

        private int lengthOne = 0;
        private int lengthTwo = 140;

        public Sms()
        {

        }
        public string Sender
        {
            set
            {
                if(!Regex.IsMatch(value, regexMatcher))
                {
                    throw new ArgumentException("SMS Sender Invalid");
                }

                else
                {
                    this.sender = value;
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
                if(value.Length > lengthOne && value.Length <= lengthTwo)
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
    }
}