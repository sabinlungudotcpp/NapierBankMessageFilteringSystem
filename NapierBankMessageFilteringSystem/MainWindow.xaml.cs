using Microsoft.Win32;
using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

// Author of System: Sabin Constantin Lungu
// Date of Last Modification: 28/10/2020
// Purpose of Class: To store the Presentation Layer code for the Message Filtering System such as sanitising SMS, E-mail and Tweet messages
// Any Bugs: N/A

namespace NapierBankMessageFilteringSystem
{

    public partial class MainWindow : Window
    {
        private char[] delimiters = { '.', ',', ' ' };
        private int defaultValue = 0;

        private Abbreviations abbreviations = new Abbreviations();
        private Message message = new Message(); // New message instance
        private Sms sms = new Sms(); // SMS instance to store SMS messages

        private Email email = new Email(); // E-mail message instance
        private Tweet tweets = new Tweet();
        private SaveFile file = new SaveFile(); // Creates a Save File instance to save the message to the file

        private List<Message> outputMessages = new List<Message>();
        private List<string> incidentList = new List<string>();
        private List<string> quarantineList = new List<string>(); // Declares a new list to store the URLs that are quarantined
        private List<string> mentionsList = new List<string>(); // List to store twitter mentions

        private Dictionary<string, int> tweetHashtags = new Dictionary<string, int>();
        
        public MainWindow() {
            Resources["TweetHashtags"] = this.tweetHashtags;
            Resources["Mentions"] = this.mentionsList;
            Resources["SIR"] = this.incidentList;
            InitializeComponent();
        }

        private void processMsgButton_Click(object sender, RoutedEventArgs e) // Process Message ID & Body manually
        {
            string[] messageTypes = { "S", "E", "T" };
            int startIndex = 0;

            try
            {
                string messageHeader = msgHeaderTxtBox.Text.ToUpper(); // The message header text to upper case
                string messageBody = msgTextBox.Text;
                bool isEmpty = false;

                message.MessageID = messageHeader; // The message ID is the message header
                message.MessageBody = messageBody;

                if (messageHeader.StartsWith(messageTypes[0]) && Char.IsUpper(Convert.ToChar(messageTypes.ElementAt(startIndex)))) // If the message header starts with an upper case S
                {
                    isEmpty = false;
                    sanitiseSms(message); // Sanitise SMS messages
                }

                else if (messageHeader.StartsWith(messageTypes[1]) && Char.IsUpper(Convert.ToChar(messageTypes.ElementAt(startIndex)))) // If the message header text box starts with an E
                {
                    isEmpty = false;
                    sanitiseEmail(message);
                }

                else if (messageHeader.StartsWith(messageTypes[2]) && Char.IsUpper(Convert.ToChar(messageTypes.ElementAt(startIndex))))
                {
                    isEmpty = false;
                    sanitiseTweets(message); // Sanitise tweet messages
                }

                else
                {
                    isEmpty = true;

                    if(isEmpty)
                    {
                        MessageBox.Show("Please fill in both boxes");
                    }
                   
                }
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void readFileBtn_Click(object sender, RoutedEventArgs e) // Method that processes the read file button
        {
            try
            {
                var filePath = string.Empty; // The file path is stored here
                var fileContent = string.Empty;
                bool isFileValid = false; // Determines if the file is valid or not

                OpenFileDialog fileDialog = new OpenFileDialog(); // A new open file dialog instance
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == true && fileDialog != null)
                {
                    filePath = Path.GetExtension(fileDialog.FileName); // Get the file extension

                    if (filePath.Equals(".txt")) // If the file path is .txt
                    {
                        isFileValid = true;

                        if (isFileValid)
                        {
                            string fileData = File.ReadAllText(fileDialog.FileName); // Read all the text from the file
                            
                        }

                        string[] fileLines = File.ReadAllLines(fileDialog.FileName);

                        foreach (string messageLines in fileLines) // For every line in the text
                        {
                            if (messageLines.Length > defaultValue || messageListBox.Items.Count == defaultValue)
                            {
                                messageListBox.Items.Add(messageLines.ToString()); // Add it to the message list box
                            }
                        }
                    }

                    else if (!filePath.EndsWith(".txt") || !filePath.EndsWith(".json")) // If the file paths are not txt or json
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

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void messageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string smsMessageID = message.MessageID;
                string smsMessageBody = message.MessageBody;

                string fileLineChosen = Convert.ToString(messageListBox.SelectedItem);
                string fileLineSplit = fileLineChosen.Split(delimiters[2])[defaultValue].ToUpper();

                sms.MessageID = fileLineSplit; // The message ID is the split file line
                
                string countryCode = fileLineChosen.Split(delimiters[2])[defaultValue + 1];
                string msgSender = fileLineChosen.Split(delimiters[2])[defaultValue + 2];
                string text = fileLineChosen.Split(delimiters[2])[defaultValue + 3];

                int indexOfText = fileLineChosen.IndexOf(text);
                string substringOfText = fileLineChosen.Substring(indexOfText);
                sms.MessageBody = substringOfText;

                abbreviations.readFile();
                string replacedMessage = abbreviations.replaceMessage(sms.MessageBody);

                messageID.Text = "Message ID : " + fileLineSplit.ToString();
                messageSender.Text = "Message Sender : " + countryCode.ToString() + ' ' + msgSender.ToString();
                messageText.Text = "Message Text : " + replacedMessage.ToString();

            }

            catch (Exception exc)
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
                sms.MessageBody = messageBody; // The SMS message body is the message body text

                string smsCountryCode = messageBody.Split(splitToken)[defaultValue]; // Split the country code.
                sms.CountryCode = smsCountryCode;
                string smsSender = messageBody.Split(splitToken)[defaultValue + 1];

                sms.Sender = smsSender; // The SMS sender is the message sms sender
                sms.MessageID = message.MessageID;
                sms.MessageBody = message.MessageBody;

                int smsIndexToProcess = messageBody.IndexOf(space) + defaultValue + 1;
                string processedSMS = messageBody.Substring(smsIndexToProcess);
                int nextIndex = processedSMS.IndexOf(space) + defaultValue + 1;

                string finalSms = processedSMS.Substring(nextIndex);
                sms.SmsText = finalSms;

                abbreviations.readFile(); // Read the abbreviations file
                string newSentence = abbreviations.replaceMessage(sms.SmsText);
                sms.SmsText = newSentence;

                outputMessages.Add(sms);
                

                if (file != null) { // If there is a file
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

            catch (Exception exc) {

                MessageBox.Show(exc.ToString());
            }

            return false;
        }

        private bool sanitiseEmail(Message message) // Routine to sanitise Email messages & SIR e-mails
        {
            try
            {
                bool isEmailSanitised = false; // Determines if the E-mail is sanitised
                string quarantineText = "<URL Quarantined>";

                if(message.MessageBody.Contains("SIR"))
                {
                    processSIREmails(email.EmailText);
                    return true;
                }
                
                string emailMessage = email.MessageID;
                string emailMsgBody = email.MessageBody;

                email.MessageID = message.MessageID;
                email.MessageBody = message.MessageBody;

                string emailID = message.MessageID; // The E-mail ID is the message ID
                string emailBody = message.MessageBody;

                emailMessage = emailID;
                emailMsgBody = emailBody;

                string emailSender = emailBody.Split(',')[defaultValue];
                string emailSubject = emailBody.Split(',')[defaultValue + 1];
                string emailText = emailBody.Split(',')[defaultValue + 2];
                
                email.EmailSender = emailSender;
                email.Subject = emailSubject;
                email.EmailText = emailText;

                foreach (string emailWord in emailText.Split(delimiters[1])) {

                    if (emailWord.Trim().Contains("http://") || emailWord.Trim().Contains("https://") || emailWord.Trim().EndsWith(".com"))
                    {
                        int urlIndexToProcess = emailText.IndexOf("http");
                        string processedURL = emailText.Substring(urlIndexToProcess);
                        int nextIndexURL = processedURL.IndexOf(" ") + 1;

                        string newSentence = emailText.Replace(emailWord, quarantineText);
                        emailText = newSentence;

                        if (quarantineListBox.Items.Count == defaultValue) {

                            int emailIndexToProcess = emailText.IndexOf(" ") + defaultValue + 1;
                            string processedEmail = emailText.Substring(emailIndexToProcess);
                            int nextIndex = processedEmail.IndexOf(" ") + defaultValue + 1;

                            string finalEmailTxt = processedEmail.Substring(nextIndex);
                            email.EmailText = finalEmailTxt;

                            abbreviations.readFile();
                            string replacedEmailTxt = abbreviations.replaceMessage(email.EmailText);
                            email.EmailText = replacedEmailTxt;

                            quarantineListBox.Items.Add(processedURL);
                        }

                        if (quarantineList != null) // If there is a quarantine list
                        {
                            quarantineList.Add(emailText); // Add the e-mails to the quarantine list
                        }
                    }
                }

                isEmailSanitised = true;

                if (isEmailSanitised)
                {
                    SaveFile emailFile = new SaveFile();

                    if (emailFile != null && outputMessages != null) // If there is an email file
                    {
                        outputMessages.Add(email);
                        emailFile.saveToJSON(outputMessages); // Write it to the JSON file
                    }

                    messageID.Text = "Message ID : " + emailID.ToString();
                    messageSender.Text = "Message Sender : " + '\n' + emailSender.ToString() + '\n' + "Message Subject : " + emailSubject.ToString();
                    messageText.Text = "Message Text : " + emailText.ToString();
                }

                if(!isEmailSanitised)
                {
                    MessageBox.Show("Could not sanitise emails");
                }

                return true;
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return false;
        }

        private bool sanitiseTweets(Message message) // Method that takes a message as the parameter and returns either true or false if the tweets have been sanitised
        {
            try
            {
                string delimiter = " "; // Space delimiter
                bool isTweetSanitised = false; // Determines if the tweet has been sanitised or not

                tweets.MessageID = message.MessageID; // The Tweet message ID
                tweets.MessageBody = message.MessageBody;

                int tweetIndex = message.MessageBody.IndexOf(delimiter) + defaultValue + 1;
                string tweetText = tweets.TweetText;
                string processedTweet = tweets.MessageBody.Substring(tweetIndex);

                tweets.TweetSender = tweets.MessageBody.Substring(defaultValue, tweets.MessageBody.IndexOf(delimiter)); // The tweet sender is the substring of the message body followed by a space: '@Sabin Lungu'
                tweets.TweetText = processedTweet;

                for (int i = defaultValue; i < processedTweet.Length; i++)
                {
                    abbreviations.readFile(); // Read the abbreviatiosn file
                    string replacedText = abbreviations.replaceMessage(tweets.TweetText); // Replace the text with the definitions
                    tweets.TweetText = replacedText;
                }

                checkForMentions(tweets.TweetSender);
                produceTrendingList(tweets.TweetText);

                isTweetSanitised = true; // The tweets are sanitised

                if (isTweetSanitised)
                {
                    messageID.Text = "Message ID : " + tweets.MessageID.ToString().Trim();
                    messageSender.Text = "Message Sender : " + tweets.TweetSender.ToString().Trim();
                    messageText.Text = "Message Text : " + tweets.TweetText.ToString().Trim();
                  }

                return true;
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            return false;
        }
        private string checkForMentions(string tweetSentence) // Function that takes the tweet sentence as the parameter and checks for tweet mentions in the body of the sentence
        {
            try
            {
                string[] splitTweetMsg = tweetSentence.Split(delimiters[2]); // Splits the tweet sentence by a space
                string atSymbol = "@";
                bool mentionFound = false;

                foreach (string tweetMessageBody in splitTweetMsg)
                {
                    if (tweetMessageBody.Contains(atSymbol) || mentionsList != null || mentionsList.Count == defaultValue) // If the tweet message body
                    {
                        for (int x = defaultValue; x < mentionsList.Count; x++)
                        {
                            bool containsMentions = mentionsList.Contains(tweetMessageBody.ToString());

                            if (containsMentions)
                            {
                                mentionsList.Add(tweetMessageBody);
                            }

                            if (!containsMentions)
                            {
                                MessageBox.Show("No mentions found");
                            }
                        }
                        
                        mentionFound = true;

                        if (mentionFound || mentionsListBox.Items.Count == defaultValue) // If a mention has been found and the mentions list box items is 0
                        {
                            mentionsListBox.Items.Add(tweetMessageBody.ToString());

                            SaveFile tweetsFile = new SaveFile();

                            if (tweetsFile != null && outputMessages != null) // If there is an existing tweets file
                            {
                                outputMessages.Add(tweets);
                                tweetsFile.saveToJSON(outputMessages);
                            }
                        }
                    }
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return tweetSentence;
        }

        private bool produceTrendingList(string tweetSentence) // Process the trending list if a hash tag is in the body of the message
        {
            try
            {
                tweetHashtags.Clear();

                string[] splitTweetMsg = tweetSentence.Split(delimiters[2]);
                string hashtag = "#";

                foreach (string tweetData in splitTweetMsg) // For every tweet in the sentence
                {
                    int currentCount; // The count of hashtags found
                    tweetHashtags.TryGetValue(tweetData, out currentCount);

                    if (tweetData.StartsWith(hashtag) && tweetHashtags != null || !tweetData.Contains(tweetData))
                    {

                        bool containsHashtag = tweetHashtags.ContainsKey(hashtag);

                        if (!containsHashtag || tweetData.Length > defaultValue)
                        {
                            tweetHashtags[tweetData] = currentCount + defaultValue + 1;
                        }
                    }
                }

                trendingListBox.ItemsSource = new Dictionary<string, int>();
                trendingListBox.ItemsSource = tweetHashtags.OrderByDescending(key => key.Value); // Shouldn't it be by current counter?
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return true;
        }

        private bool processSIREmails(string emailSirSentence) // Method that processes SIR e-mails.
        {
            try
            {
                bool foundSIR = false;
                string replaceRegex = "@/s+"; // This represents the dash (-) to split the SIR body
                string fileLine = string.Empty;
                string sirFilePath = "C:/Users/const/Desktop/NapierBankMessageFilteringSystem-main/NapierBankMessageFilteringSystem/SIRList.csv";

                // Read file that contains SIGNIFICANT INCIDENT REPORTS
                StreamReader sirFile = new StreamReader(sirFilePath);
                while ((fileLine = sirFile.ReadLine()) != null && incidentList != null)
                {
                    if (fileLine.Length > defaultValue)
                    {
                        incidentList.Add(fileLine);
                    }
                }

                string emailText = email.EmailText; // The Email Text
                string emailMsgID = message.MessageID;
                emailSirSentence = message.MessageBody; // The SIR email sentence
                
                string emailSender = emailSirSentence.Split(',')[defaultValue].Trim();
                string emailSubject = emailSirSentence.Split(',')[defaultValue + 1].Trim();
                string sortCode = emailSirSentence.Split(',')[defaultValue + 2].Trim(); // Sort code is split at index 2
                string incident = emailSirSentence.Split(',')[defaultValue + 3].Trim(); // The incident type is split
                string emailBody = emailSirSentence.Split(',')[defaultValue + 4].Trim(); // The e-mail body split
                string replacedSIR = Regex.Replace(sortCode, replaceRegex, "");

                foreach (string natureOfIncident in incidentList) // For every incident in the incident list
                {
                    if (natureOfIncident.Length > defaultValue)
                    {
                        if (natureOfIncident.Equals(incident))
                        {
                            foundSIR = true;
                        }
                    }
                }

                if (emailSubject.Contains("SIR") && foundSIR && sirListBox.Items.Count == defaultValue)
                {
                    sirListBox.Items.Add(sortCode + ' ' + incident); // Adds the sort code and nature of incident to the SIR list
                }

                if (!emailSubject.Contains("SIR") || !foundSIR)
                {
                    MessageBox.Show("Incident Not Found In The File");
                    foundSIR = false; // SIR not found so is false
                }

                messageID.Text = "Message ID : " + emailMsgID.ToString();
                messageSender.Text = "Message Sender : " + '\n' + emailSender.ToString() + '\n' + "Message Subject : " + emailSubject.ToString() + '\n' + "Sort Code" + sortCode;
                messageText.Text = "Message Text : " + emailBody.ToString();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

            return true;
        }

       private void clearData(object sender, RoutedEventArgs e) // Clears data from the system
        {
            try
            {
                System.Windows.Forms.DialogResult clearDialog = (System.Windows.Forms.DialogResult)MessageBox.Show("Are you sure you want to clear the system data ? ", "Warning", MessageBoxButton.YesNo);

                switch(clearDialog)
                {

                    case System.Windows.Forms.DialogResult.Yes:

                        // Clear the data from the system
                        messageID.Text = string.Empty;
                        messageSender.Text = string.Empty;
                        messageText.Text = string.Empty;
                       
                        for (int x = defaultValue; x < mentionsListBox.Items.Count; x++)
                        {
                            if (mentionsListBox.Items.Count > defaultValue || mentionsListBox != null)
                            {
                                mentionsListBox.Items.Clear();
                                tweets = null;
                            }
                        }

                        for (int y = defaultValue; y < messageListBox.Items.Count; y++)
                        {
                            if (messageListBox.Items.Count > defaultValue || messageListBox != null)
                            {
                                messageListBox.Items.Clear();
                                message = null;
                            }
                        }

                        for (int z = defaultValue; z < quarantineListBox.Items.Count; z++)
                        {
                            if (quarantineListBox.Items.Count > defaultValue || quarantineListBox != null)
                            {
                                quarantineListBox.Items.Clear();
                                email = null;
                            }
                        }

                        for (int i = defaultValue; i < trendingListBox.Items.Count; i++)
                        {
                            if (tweetHashtags.Count > defaultValue || trendingListBox != null)
                            {
                                trendingListBox.ItemsSource = string.Empty;
                                tweets = null; // Tweet instance is empty now
                            }
                        }

                        msgHeaderTxtBox.Text = string.Empty;
                        msgTextBox.Text = string.Empty;
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        break;
                }
            }

            catch (Exception exc)
            {
                MessageBox.Show("An error occurred clearing data from the system" + ' ' + exc.ToString());
            }
        }
    }
}