using NapierBankMessageFilteringSystem.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    // Author of Class: Sabin Constantin Lungu
    // Purpose of Class: To read JSON file, deserialize it and store it in a list box to be processed
    // 
    [Serializable()]
    public class SaveFile // Class to save messages to a JSON file
    {
        private bool serialized = false;

        public void saveToJSON(List<Message> inputMessages) // Method that saves a type of message to a JSON file by serializing it
        {
            try
            {
                File.WriteAllText("C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/messagesFile.json", JsonConvert.SerializeObject(inputMessages));

                using (StreamWriter messageFile = File.CreateText("C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/messagesFile.json"))
                {
                    if (messageFile != null && inputMessages != null)
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(messageFile, inputMessages); // Serialize the messaegs to the file
                        serialized = true;
                    }
                }
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}