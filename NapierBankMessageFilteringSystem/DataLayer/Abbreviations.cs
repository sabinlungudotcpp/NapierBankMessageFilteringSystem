﻿using System;
using System.Collections.Generic;
using System.IO;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    // Author: Sabin Constantin Lungu
    // Date of creation: 27/10/2020
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
        }

        public string replaceMessage(string sentence) // Replaces the definition with the actual word
        {
            char splitToken = ' ';
            
            try
            {
                foreach (string definitionWord in sentence.Split(splitToken)) // For every definition in the sentence
                {
                   
                 foreach(string abbreviation in abbreviationsList) // And for every abbreviation in the list
                   {
                        if(definitionWord.Equals(abbreviation) && listOfDefinitions.Count > 0 && abbreviationsList.Count > 0)
                        {
                            int indexOfDefinition = abbreviationsList.IndexOf(abbreviation);
                            string allDefinitions = listOfDefinitions[indexOfDefinition];
                            string replacedAbbreviations = abbreviation + " <" + allDefinitions + " >"; // Replaces the abberviations with all the 

                            int newIndex = sentence.IndexOf(definitionWord);

                            string newSentence = sentence.Replace(definitionWord, replacedAbbreviations);
                            sentence = newSentence;

                            foundAbbreviation = true;
                        }

                        else if(!definitionWord.Equals(abbreviation))
                        {
                            foundAbbreviation = false;
                            throw new Exception("Could not find abbreviation");

                        }
                   }

                }
            }

            catch
            {
                return sentence;
            }

            return sentence;

        }
    }
}
