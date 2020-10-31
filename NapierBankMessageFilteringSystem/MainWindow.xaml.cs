using Microsoft.Win32;
using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

// Author of System: Sabin Constantin Lungu
// Date of Last Modification: 28/10/2020
// Purpose of Class: 
// Any Bugs: N/A

namespace NapierBankMessageFilteringSystem
{
 
    public partial class MainWindow : Window
    {
        private char[] delimiters = { '.', ',', ' '};
        private int defaultValue = 0;

        private Abbreviations abbreviations = new Abbreviations();
        private Message message = new Message(); // New message instance
        private Sms sms = new Sms();
        private Email email = new Email(); // E-mail message instance
        private Tweet tweets = new Tweet();

        private List<string> incidentList = new List<string>();
        private List<string> quarantineList = new List<string>(); // Declares a new list to store the URLs that are quarantined
        private List<string> mentionsList = new List<string>(); // List to store twitter mentions

        private Dictionary<string, string> SIR = new Dictionary<string, string>();
        private Dictionary<string, int> tweetHashtags = new Dictionary<string, int>();

        private List<Message> outputMessages = new List<Message>();

        public MainWindow()
        {
            Resources["TweetHashtags"] = tweetHashtags;
            InitializeComponent();
        }

        private void processMsgButton_Click(object sender, RoutedEventArgs e) // Process Message ID & Body manually
        {

            string[] messageTypes = { "S", "E", "T" };

            try
            {
                string messageHeader = msgHeaderTxtBox.Text.ToUpper();
                string messageBody = msgTextBox.Text;
                bool isEmpty = false;

                message.MessageID = messageHeader;
                message.MessageBody = messageBody;

                if(messageHeader.StartsWith(messageTypes[0])) // If the message header starts with an upper case S
                {
                    sanitiseSms(message); // Sanitise SMS messages
                }

                else if(messageHeader.StartsWith(messageTypes[1])) // If the message header text box starts with an E
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

                OpenFileDialog fileDialog = new OpenFileDialog(); // A new open file dialog instance
                fileDialog.RestoreDirectory = true;

                if(fileDialog.ShowDialog() == true && fileDialog != null)
                {
                    filePath = Path.GetExtension(fileDialog.FileName); // Get the file extension

                    if(filePath.Equals(".txt")) // If the file path is .txt
                    {
                        isFileValid = true;

                        if(isFileValid)
                        {
                            string fileData = File.ReadAllText(fileDialog.FileName);
                        }

                        foreach(string lines in File.ReadAllLines(fileDialog.FileName))
                        {
                            if(lines.Length > defaultValue || messageListBox.Items.Count == defaultValue)
                            {
                                messageListBox.Items.Add(lines);
                            }
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
                string messageID = message.MessageID;
                string fileLineChosen = Convert.ToString(messageListBox.SelectedItem);
                string fileLineSplit = fileLineChosen.Split(delimiters[2])[defaultValue].ToUpper();
               
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private bool sanitiseSms(Message message) // Routine to sanitise SMS messages
        {
           try
            {
               bool isSmsSanitised = false; // Flag to determine if the SMS message is sanitised or not
               char splitToken = ' '; // Space character to split the data
               string space = " ";

               string smsID = sms.MessageID; // The SMS ID
               string msgID = message.MessageID;
               smsID = msgID;

               string smsBody = sms.MessageBody;
               string messageBody = message.MessageBody;
               smsBody = messageBody;

               string smsCountryCode = smsBody.Split(splitToken)[defaultValue]; // Split the country code.
               string smsSender = smsBody.Split(splitToken)[defaultValue + 1];

               int smsIndexToProcess = smsBody.IndexOf(space) + defaultValue + 1;
               string processedSMS = smsBody.Substring(smsIndexToProcess);
               int nextIndex = processedSMS.IndexOf(space) + defaultValue + 1;

               string finalSms = processedSMS.Substring(nextIndex);
               sms.SmsText = finalSms;

               string newSentence = abbreviations.replaceMessage(sms.SmsText);
               sms.SmsText = newSentence;

               outputMessages.Add(sms);
               abbreviations.readFile();

               SaveFile file = new SaveFile();

                if(file != null) {
                    file.saveToJSON(outputMessages);
                }
              
               isSmsSanitised = true;

               if (isSmsSanitised) {
                   messageID.Text = "Message ID : " + smsID.ToString();
                   messageSender.Text = "Message Sender : " + smsCountryCode.ToString() + splitToken + smsSender;
                   messageText.Text = "Message Text : " + sms.SmsText.ToString();
                }

                return true;
            } 
            
            catch(Exception exc) {
            
                MessageBox.Show(exc.ToString());
            }

            return false;
        }

        private bool sanitiseEmail(Message message) // Routine to sanitise Email messages & SIR e-mails
        {
            try
            {
                bool isEmailSanitised = false; // Determines if the E-mail is sanitised
                char splitToken = ',';
                string quarantineText = "<URL Quarantined>";

                string fileLine = string.Empty;
                string sirFilePath = "C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/NapierBankMessageFilteringSystem/SIRList.csv";

                // Read file that contains SIGNIFICANT INCIDENT REPORTS
                StreamReader sirFile = new StreamReader(sirFilePath);
                while((fileLine = sirFile.ReadLine()) != null && incidentList != null)
                {
                    string[] sirData = fileLine.Split(splitToken);
                    incidentList.Add(sirData[0]);
                }

                string emailID = message.MessageID; // The E-mail ID is the message ID
                string emailBody = message.MessageBody;
                
                string emailSender = emailBody.Split(splitToken)[defaultValue];
                string emailSubject = emailBody.Split(splitToken)[defaultValue+1];
                string emailText = emailBody.Split(splitToken)[defaultValue+2];

                processSIREmails(email.EmailText);

                foreach(string emailWord in emailText.Split(splitToken)) {
                    
                    if (emailWord.Trim().Contains("http://") || emailWord.Trim().Contains("https://") || emailWord.Trim().EndsWith(".com"))
                    {
                        string newSentence = emailText.Replace(emailWord, quarantineText);
                        emailText = newSentence;

                        if(quarantineListBox.Items.Count == defaultValue) {
                        
                            int smsIndexToProcess = emailBody.IndexOf(" ") + defaultValue + 1;
                            string processedSMS = emailBody.Substring(smsIndexToProcess);
                            int nextIndex = processedSMS.IndexOf(" ") + defaultValue + 1;

                            string finalEmailTxt = processedSMS.Substring(nextIndex);
                            email.EmailText = finalEmailTxt;

                            abbreviations.readFile();
                            string replacedEmailTxt = abbreviations.replaceMessage(email.EmailText);
                            email.EmailText = replacedEmailTxt;

                            quarantineListBox.Items.Add(email.EmailText.ToString());
                        }

                        if (quarantineList != null) // If there is a quarantine list
                        {
                            quarantineList.Add(emailText); // Add the e-mails to the quarantine list
                        }
                    }
                }

                outputMessages.Add(message);
                SaveFile emailFile = new SaveFile();

                if(emailFile != null)
                {
                    emailFile.saveToJSON(outputMessages);
                }
                
                isEmailSanitised = true;

                if (isEmailSanitised)
                {
                    messageID.Text = "Message ID : " + emailID.ToString();
                    messageSender.Text = "Message Sender:  " + emailSender.ToString();
                    messageText.Text = "Message Text: " + emailSubject.ToString() + splitToken + emailText.ToString();
                }

                return true;
            } 
            
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return false;
        }

        private bool sanitiseTweets(Message message)
        {
            try
            {
                string delimiter = " "; // Space delimiter
                bool isTweetSanitised = false; // Determines if the tweet has been sanitised or not

                tweets.MessageID = message.MessageID;
                tweets.MessageBody = message.MessageBody;

                int tweetIndex = message.MessageBody.IndexOf(delimiter) + defaultValue + 1;
                string tweetText = tweets.TweetText;
                string processedTweet = tweets.MessageBody.Substring(tweetIndex);

                tweets.TweetSender = tweets.MessageBody.Substring(defaultValue, tweets.MessageBody.IndexOf(delimiter)); // The tweet sender is the substring of the message body followed by a space: '@Sabin Lungu'
                tweets.TweetText = processedTweet;

                for (int i = defaultValue; i < processedTweet.Length; i++)
                {
                    abbreviations.readFile();
                    string replacedText = abbreviations.replaceMessage(tweets.TweetText);
                    tweets.TweetText = replacedText;
                }

                checkForMentions(tweets.TweetSender);
                produceTrendingList(tweets.TweetText);

                isTweetSanitised = true; // The tweets are sanitised

                if(isTweetSanitised)
                {
                    messageID.Text = "Message ID : " + tweets.MessageID.ToString();
                    messageSender.Text = "Message Sender : " + tweets.TweetSender.ToString();
                    messageText.Text = "Message Text : " + tweets.TweetText.ToString();
;                }

                return true;
            }
           
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            return false;
        }
        private string checkForMentions(string tweetSentence)
        {
            string[] splitTweetMsg = tweetSentence.Split(delimiters[2]);
            string atSymbol = "@";
            bool mentionFound = false;

            foreach (string tweetMessageBody in splitTweetMsg)
            {
                if(tweetMessageBody.Contains(atSymbol) || mentionsList != null || mentionsList.Count == defaultValue) // If the tweet message body
                {
                    for(int x = defaultValue; x < mentionsList.Count; x++)
                    {
                        bool containsMentions = mentionsList.Contains(tweetMessageBody.ToString());

                        if(containsMentions)
                        {
                            mentionsList.Add(tweetMessageBody);
                        }
                    }

                    mentionsListBox.Items.Add(tweetMessageBody);
                    mentionFound = true;

                    if(mentionFound)
                    {
                        SaveFile tweetsFile = new SaveFile();
                        if(tweetsFile != null)
                        {
                            outputMessages.Add(tweets);
                            tweetsFile.saveToJSON(outputMessages);
                        }
                    }
                }
            }

            return tweetSentence;
        }

        private bool produceTrendingList(string tweetSentence) // Process the trending list if a hash tag is in the body of the message
        {
            tweetHashtags.Clear();
            string[] splitTweetMsg = tweetSentence.Split(delimiters[2]);
            string hashtag = "#";

            foreach(string tweetData in splitTweetMsg)
            {
                int currentCount;
                tweetHashtags.TryGetValue(tweetData, out currentCount);

                if (tweetData.StartsWith(hashtag) && tweetHashtags != null || !tweetData.Contains(tweetData)) {
                    tweetHashtags[tweetData] = currentCount + defaultValue + 1;
                }
            }

            trendingListBox.ItemsSource = new Dictionary<string, int>();
            trendingListBox.ItemsSource = tweetHashtags.OrderByDescending(key => key.Value); // Shouldn't it be by current counter?

            return true;
        }

        private bool processSIREmails(string emailSentence)
        {

            // string[] splitEmailMsg = emailSentence.Split(delimiters[2]); // Split the e-mail by a comma.
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Clears data from the system
        {
            // Objects are now empty
            sms = null;
            
            // Clear the data from the system
            messageID.Text = string.Empty;
            messageSender.Text = string.Empty;
            messageText.Text = string.Empty;

            for(int x = defaultValue; x < mentionsListBox.Items.Count; x++)
            {
                if(mentionsListBox.Items.Count > defaultValue || mentionsListBox != null)
                {
                    mentionsListBox.Items.Clear();
                    tweets = null;
                }
            }

            for(int y = defaultValue; y < messageListBox.Items.Count; y++)
            {
                if(messageListBox.Items.Count > defaultValue || messageListBox != null)
                {
                    messageListBox.Items.Clear();
                    message = null;
                }
            }

            for(int z = defaultValue; z < quarantineListBox.Items.Count; z++)
            {
                if(quarantineListBox.Items.Count > defaultValue || quarantineListBox != null)
                {
                    quarantineListBox.Items.Clear();
                    email = null;
                }
            }

            msgHeaderTxtBox.Text = string.Empty;
            msgTextBox.Text = string.Empty;
        }
    }
}