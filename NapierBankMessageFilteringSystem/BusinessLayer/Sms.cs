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
        private string sender;
        private string countryCode; // The SMS country code
        private string smsText; // The SMS text

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

            }

            get
            {
                return this.countryCode;
            }
        }

        private bool isSenderValid()
        {
            return Regex.IsMatch(this.sender, regexMatcher); // Returns true or false if the sender matches the regular expressions
        }
    }
}
