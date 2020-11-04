using System;
using System.Collections.Generic;
using System.IO;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 27/10/2020
    // Date of last modification: 28/10/2020
    // Errors: N/A
    public class Abbreviations // Abbreviations class that replaces an abbreviation with its actual definition
    {
        private List<string> listOfDefinitions = new List<string>(); // List of definitions
        private List<string> abbreviationsList = new List<string>(); // List of abbreviations

        private string textWordsFile = "C:/Users/const/Desktop/NapierBankMessageFilteringSystem/textwords.csv";
        private char delimiter = ',';
        private int defaultValue = 0;

        public bool readFile() // Read the file that contains the abbreviations
        {
            StreamReader abbreviationsFile = new StreamReader(textWordsFile); // Create a stream reader to read in the file
            string fileLine;

            while((fileLine = abbreviationsFile.ReadLine()) != null)
            {
                string[] textValues = fileLine.Split(delimiter); // Split the data by a comma delimiter.

                abbreviationsList.Add(textValues[defaultValue]); // Add the abbreviations from the file to the list
                listOfDefinitions.Add(textValues[defaultValue + 1]);
            }

            return true;
        }

        public string replaceMessage(string sentence) // Replaces the definition with the actual word
        {
           char splitToken = ' '; // Space token
           string[] splitSentence = sentence.Split(splitToken);
            
           try
            {
                foreach (string definitionWord in splitSentence) // For every definition word in the sentence
                {
                   
                 foreach(string abbreviation in abbreviationsList) // And for every abbreviation in the list
                   {
                        if(definitionWord.Equals(abbreviation) && listOfDefinitions.Count > defaultValue && abbreviationsList.Count > defaultValue && abbreviationsList != null)
                        {
                            int indexOfDefinition = abbreviationsList.IndexOf(abbreviation);
                            string allDefinitions = listOfDefinitions[indexOfDefinition]; // Store the list of all the definitions in the string array
                            string replacedAbbreviations = " <".Trim() + allDefinitions + " >".Trim(); // Replaces the abberviations with all the 

                            int newIndex = sentence.IndexOf(definitionWord); // Get the index of the definition

                            string newSentence = sentence.Replace(definitionWord, replacedAbbreviations); // Replace the abbreviation with the definition
                            sentence = newSentence;
                        }
                     }
                }
            }

            catch
            {
                throw new Exception("Error processing abbreviations");
            }

            return sentence; // Return the processed sentence
        }
    }
}