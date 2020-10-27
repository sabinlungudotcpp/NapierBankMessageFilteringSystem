using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    public class Abbreviations // Abbreviations class that replaces an abbreviation with its actual definition
    {
        public List<string> listOfDefinitions = new List<string>();
        public List<string> abbreviationsList = new List<string>();

        private bool foundAbbreviation = false;
        private bool isFileRead = false;
        private string textWordsFile = "C:/Users/const/Desktop/NapierBankMessageFilteringSystem/textwords.csv";

        public void readFile() // Read the file that contains the abbreviations
        {
            StreamReader abbreviationsFile = new StreamReader(textWordsFile); // Create a stream reader to read in the file
            string fileLine;

            while((fileLine = abbreviationsFile.ReadLine()) != null)
            {
                string[] textValues = fileLine.Split(',');

                abbreviationsList.Add(textValues[0]);
                listOfDefinitions.Add(textValues[1]);

                isFileRead = true; // File is read successfully
            }

            foreach(string def in listOfDefinitions)
            {
                MessageBox.Show(def.ToString());
            }
        }

        public string replaceMessage(string sentence)
        {
            return "";
        }


    }
}
