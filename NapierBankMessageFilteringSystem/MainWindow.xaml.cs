using Microsoft.Win32;
using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace NapierBankMessageFilteringSystem
{
 
    public partial class MainWindow : Window
    {
        public Abbreviations abbreviations = new Abbreviations();

        private Message message = new Message(); // New message instance
        private Sms sms = new Sms(); // New SMS instance
        private Email email = new Email(); // E-mail message instance
        private Tweet tweets = new Tweet();

        private List<string> incidentList = new List<string>();
        private List<string> quarantineList = new List<string>(); // Declares a new list to store the URLs that are quarantined
        private List<string> mentionsList = new List<string>();

        private Dictionary<string, string> SIR = new Dictionary<string, string>();
        private Dictionary<string, int> tweetHashtags = new Dictionary<string, int>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void processMsgButton_Click(object sender, RoutedEventArgs e) // Process Message ID & Body manually
        {
            string emptyToken = "";
            bool isEmpty = false;
            string[] messageTypes = { "S", "E", "T" };

            try
            {
                string messageHeader = msgHeaderTxtBox.Text.ToUpper();
                string messageBody = msgTextBox.Text;

                message.messageID = messageHeader;
                message.messageBody = messageBody;

                if(messageHeader.StartsWith(messageTypes[0])) // If the message header starts with an upper case S
                {
                    sanitiseSms(message);
                }

                else if(messageHeader.StartsWith(messageTypes[1]))
                {
                    sanitiseEmail(message);
                }

                else if(messageHeader.StartsWith(messageTypes[2]))
                {
                    sanitiseTweets(message); // Sanitise tweet messages
                }

                else
                {
                    MessageBox.Show("Please fill in both boxes");
                }
            } 
            
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void readFileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = string.Empty;
                var fileContent = string.Empty;
                bool isFileValid = false;

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.RestoreDirectory = true;

                if(fileDialog.ShowDialog() == true && fileDialog != null)
                {
                    filePath = Path.GetExtension(fileDialog.FileName);

                    if(filePath.Equals(".txt")) // If the file path is .txt
                    {
                        isFileValid = true;
                        string fileData = File.ReadAllText(fileDialog.FileName);

                        foreach(string lines in File.ReadAllLines(fileDialog.FileName))
                        {
                            messageListBox.Items.Add(lines);
                        }
                    }

                    else if(!filePath.Equals(".txt") || !filePath.Equals(".json")) // If the file paths are not txt or json
                    {
                        isFileValid = false; // File not valid
                        MessageBox.Show("Please choose a .txt file or .json");
                    }

                    else
                    {
                        MessageBox.Show("Invalid file type");
                    }
                }
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void messageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void sanitiseSms(Message message) // Routine to sanitise SMS messages
        {
           try
            {
                bool isSmsSanitised = false;

                if(message.isIdValid() && message.isBodyValid()) // If the message ID is valid and the message body is valid
                {
                    char splitToken = ' '; // Space character to split the data
                    int index = 0;

                    string smsSenderID = sms.messageID; // The SMS Sender ID is the message ID
                    string messageID = message.messageID; // The message ID of the message

                    string smsBody = sms.messageBody; // Body of the message
                    string msgBody = message.messageBody;
                    string smsCode = smsBody.Split(splitToken)[index]; // Split the SMS country code
                    string smsText = smsBody.Split(splitToken)[index + 1]; // Split the SMS text by a space after the country code
                                  
                    smsSenderID = messageID;
                    smsBody = msgBody;
                }

                else if(!message.isIdValid() || message.isBodyValid())
                {
                    isSmsSanitised = false;
                    MessageBox.Show("The Message ID or the body is invalid. Please re-check your entries");
                }
              
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void sanitiseEmail(Message message) // Routine to sanitise Email messages
        {
            try
            {

            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void sanitiseTweets(Message message)
        {

        }

        public string verifyURL(string sentence)
        {
            return sentence;
        }
    }
}