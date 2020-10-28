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
        private string regexMatcher = "^([0|/+[0-9]{1,5})?([7-9][0-9]{9})$";
        public string sender; // The SMS Sender
        public string countryCode; // The SMS country code
        public string smsText; // The SMS text
        private string code = "+";

        public Sms()
        {

        }

        public string Sender
        {
            set
            {
                if(isSenderValid())
                {
                    this.sender = value;
                }

                else if(!isSenderValid())
                {
                    throw new ArgumentException("Sender is invalid. Please re-enter");
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
                if(isCountryCodeValid())
                {
                    value = this.countryCode; // Set the country code to its value property
                }

                else if(!isCountryCodeValid())
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
                if(isSmsTextValid())
                {
                    value = this.smsText; // Set the SMS text to its value property
                }

                else if(!isSmsTextValid())
                {
                    throw new ArgumentException("The SMS text must have more than 0 characters and a maximum of 140");
                }
            }

            get
            {
                return this.smsText; // Return the SMS text
            }
        }

        private bool isSenderValid()
        {
            return Regex.IsMatch(this.sender, regexMatcher); // Returns true or false if the sender matches the regular expressions
        }

        public bool isCountryCodeValid() // Determines if the country code is valid
        {
            return this.countryCode.StartsWith(code);
        }

        private bool isSmsTextValid() // Determines if the SMS text is valid
        {
            return this.smsText.Length > 0 && this.smsText.Length <= 140; // Text should be between 0 and 140 characters long.
        }
    }
}
