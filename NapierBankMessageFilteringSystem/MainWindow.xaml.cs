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
        private Abbreviations abbreviations = new Abbreviations();
        private Message message = new Message(); // New message instance

        private Sms sms = new Sms(); // New SMS instance
        private Email email = new Email(); // E-mail message instance
        private Tweet tweets = new Tweet();

        private List<string> incidentList = new List<string>();
        private List<string> quarantineList = new List<string>(); // Declares a new list to store the URLs that are quarantined
        private List<string> mentionsList = new List<string>(); // List to store twitter mentions

        private Dictionary<string, string> SIR = new Dictionary<string, string>();
        private Dictionary<string, int> tweetHashtags = new Dictionary<string, int>();

        private List<string> messageInputs = new List<string>(); // Stores the input messages in a list to be thens tored in a JSON file
        private List<string> messageOutputs = new List<string>();

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
                            if(lines.Length > 0 || messageListBox.Items.Count == 0)
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
                    int smsIndex = 0;

                    string smsID = sms.MessageID; // The SMS ID
                    string msgID = message.MessageID;
                    smsID = msgID;

                    string smsBody = sms.MessageBody;
                    string messageBody = message.MessageBody;
                    smsBody = messageBody;

                    string smsCountryCode = messageBody.Split(splitToken)[smsIndex]; // Split the country code.
                    string smsSender = messageBody.Split(splitToken)[smsIndex + 1];

                    int smsIndexToProcess = smsBody.IndexOf(" ") + smsIndex + 1;
                    string processedSMS = smsBody.Substring(smsIndexToProcess);
                    int nextIndex = processedSMS.IndexOf(" ") + smsIndex + 1;

                    string finalSms = processedSMS.Substring(nextIndex);
                    sms.SmsText = finalSms;

                    string newSentence = abbreviations.replaceMessage(sms.SmsText);
                    sms.SmsText = newSentence;

                    isSmsSanitised = true;
                    
                    for(int i = 0; i < processedSMS.Length; i++) {
                    abbreviations.readFile();
                    messageInputs.Add(sms.SmsText); // Adds the messages inputs to the list
                 }

                 
                if (isSmsSanitised)
                {
                    messageID.Text = "Message ID : " + smsID.ToString();
                    messageSender.Text = "Message Sender : " + smsCountryCode.ToString() + splitToken + smsSender;
                    messageText.Text = "Message Text : " + sms.SmsText.ToString();
                }
                return true;
            } 
            
            catch(Exception exc)
            {
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

                // Read file that contains incident reports
                StreamReader sirFile = new StreamReader(sirFilePath);
                while((fileLine = sirFile.ReadLine()) != null && incidentList != null)
                {
                    string[] sirData = fileLine.Split(splitToken);
                    incidentList.Add(sirData[0]);
                }

                string emailID = message.MessageID; // The E-mail ID is the message ID
                string emailBody = message.MessageBody;
                
                string emailSender = emailBody.Split(splitToken)[0];
                string emailSubject = emailBody.Split(splitToken)[1];
                string emailText = emailBody.Split(splitToken)[2];

               
                foreach(string emailWord in emailText.Split(splitToken)) {
                    
                    if (emailWord.Trim().Contains("http://") || emailWord.Trim().Contains("https://") || emailWord.Trim().EndsWith(".com"))
                    {
                        string newSentence = emailText.Replace(emailWord, quarantineText);
                        emailText = newSentence;

                        if(quarantineListBox.Items.Count == 0)
                        {

                            int smsIndexToProcess = emailBody.IndexOf(" ") + 1;
                            string processedSMS = emailBody.Substring(smsIndexToProcess);
                            int nextIndex = processedSMS.IndexOf(" ") + 1;

                            string finalEmailTxt = processedSMS.Substring(nextIndex);
                            sms.SmsText = finalEmailTxt;

                            abbreviations.readFile();
                            string replacedEmailTxt = abbreviations.replaceMessage(sms.SmsText);
                            sms.SmsText = replacedEmailTxt;

                            quarantineListBox.Items.Add(sms.SmsText.ToString());
                        }

                        if (quarantineList != null) // If there is a quarantine list
                        {
                            quarantineList.Add(emailText); // Add the e-mails to the quarantine list
                        }
                    }
                }

                messageInputs.Add(emailText);
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
                int tweetIndex = message.MessageBody.IndexOf(delimiter) + 1;
                string tweetText = tweets.TweetText;
                string processedTweet = tweets.MessageBody.Substring(tweetIndex);

                tweets.TweetSender = tweets.MessageBody.Substring(0, tweets.MessageBody.IndexOf(delimiter)); // The tweet sender is the substring of the message body followed by a space: '@Sabin Lungu'
                tweets.TweetText = processedTweet;

                for (int i = 0; i < processedTweet.Length; i++)
                {
                    abbreviations.readFile();
                    string replacedText = abbreviations.replaceMessage(tweets.TweetText);
                    tweets.TweetText = replacedText;

                    if(messageInputs.Count > 0 || messageInputs != null)
                    {
                        messageInputs.Add(replacedText);
                    }
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
            bool mentionFound = false;

            foreach (string tweetMessageBody in splitTweetMsg)
            {
                if(tweetMessageBody.Contains("@") || mentionsList != null || mentionsList.Count == 0) // If the tweet message body
                {
                    for(int x = 0; x < mentionsList.Count; x++)
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
                        // Write to JSON file the processed tweet messages
                    }
                }
            }

            return tweetSentence;
        }

        private bool produceTrendingList(string tweetSentence) // Process the trending list if a hash tag is in the body of the message
        {
            tweetHashtags.Clear();
            string[] splitTweetMsg = tweetSentence.Split(delimiters[2]);

            foreach(string tweetData in splitTweetMsg)
            {
                int currentCount;
                tweetHashtags.TryGetValue(tweetData, out currentCount);

                if (tweetData.StartsWith("#")) {
                    tweetHashtags[tweetData] = currentCount + 1;
                }
            }

            trendingListBox.ItemsSource = new Dictionary<string, int>();
            trendingListBox.ItemsSource = tweetHashtags.OrderByDescending(key => key.Value); // Shouldn't it be by current counter?

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Clears data from the system
        {
            // Objects are now empty
            message = null;
            sms = null;
            email = null;
            tweets = null;

            // Clear the data from the system
            messageID.Text = string.Empty;
            messageSender.Text = string.Empty;
            messageText.Text = string.Empty;

            for(int i = 0; i < mentionsListBox.Items.Count; i++)
            {
                if(mentionsListBox.Items.Count > 0 || mentionsListBox != null)
                {
                    mentionsListBox.Items.Clear();
                }
            }

            messageListBox.Items.Clear();
            quarantineListBox.Items.Clear();

            msgHeaderTxtBox.Text = string.Empty;
            msgTextBox.Text = string.Empty;
        }
    }
}