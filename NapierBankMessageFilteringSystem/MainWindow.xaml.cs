using Microsoft.Win32;
using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
                message.messageBody = messageBody; // The message body

                if(messageHeader.StartsWith(messageTypes[0])) // If the message header starts with an upper case S
                {
                    sanitiseSms(message); // Sanitise SMS messages 
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
                    MessageBox.Show("Please enter a valid message");
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

                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.RestoreDirectory = true;

                if(fileDialog.ShowDialog() == true)
                {
                    filePath = Path.GetExtension(fileDialog.FileName);

                    if(filePath.Equals(".txt"))
                    {
                        string fileData = File.ReadAllText(fileDialog.FileName);

                        foreach(string lines in File.ReadAllLines(fileDialog.FileName))
                        {
                            messageListBox.Items.Add(lines);
                        }
                       
                    }

                    else if(!filePath.Equals(".txt") || !filePath.Equals(".json"))
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

        }

        private void sanitiseSms(Message message) // Routine to sanitise SMS messages
        {
           try
            {
              

            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void sanitiseEmail(Message message) // Routine to sanitise Email messages
        {

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