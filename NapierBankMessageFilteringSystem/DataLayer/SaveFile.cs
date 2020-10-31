using NapierBankMessageFilteringSystem.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessageFilteringSystem.DataLayer
{
    [Serializable()]
    public class SaveFile // Class to save messages to a JSON file
    {

        public void saveToJSON(List<Message> inputMessages)
        {
            File.WriteAllText("C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/messagesFile.json", JsonConvert.SerializeObject(inputMessages));

            using(StreamWriter messageFile = File.CreateText("C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/messagesFile.json"))
            {
                if(messageFile != null)
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(messageFile, inputMessages); // Serialize the messaegs to the file
                }
               
            }
        }
    }
}
