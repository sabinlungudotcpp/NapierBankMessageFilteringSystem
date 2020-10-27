using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    public class Abbreviations // Abbreviations class that replaces an abbreviation with its actual definition
    {
        public List<string> messagesList;
        public List<string> abbreviationsList;
        private bool foundAbbreviation;
        private string filePath = "";
    }

    public void readFile() // Read the file that contains the abbreviations
    {

    }

    public string replaceMessage(string sentence)
    {
        return "";
    }


}
