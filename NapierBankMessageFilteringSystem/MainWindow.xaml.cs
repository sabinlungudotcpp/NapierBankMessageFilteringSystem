using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Sms sms = new Sms(); // New SMS instance
        public Email email = new Email(); // E-mail message instance
        public Tweet tweets = new Tweet();

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
            int stringIndex = 0;
            bool isEmpty = false;

            try
            {
                if(msgHeaderTxtBox.Text.Equals(emptyToken) && msgTextBox.Text.Equals(emptyToken))
                {
                    isEmpty = true;

                    if(isEmpty)
                    {
                        MessageBox.Show("Message ID and Body cannot be left empty.Re-enter please");
                    }
                }

                if(!msgHeaderTxtBox.Text.Equals(emptyToken) && msgTextBox.Text.Equals(emptyToken) && message.isIdValid())
                {
                    string messageHeaderText = msgHeaderTxtBox.Text.ToUpper();
                    string messageBody = msgTextBox.Text; // The text for the message body.

                    message.messageID = messageHeaderText;
                    message.messageBody = messageBody; // The message body

                    
                }

                
                else
                {
                    MessageBox.Show("Both boxes should be entered with a message");
                }
            } 
            
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void readFileBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sanitiseSms(Message message) // Routine to sanitise SMS messages
        {
            
        }

        private void sanitiseEmail(Message message) // Routine to sanitise Email messages
        {

        }

        private void sanitiseTweets(Message message)
        {

        }


    }
}
