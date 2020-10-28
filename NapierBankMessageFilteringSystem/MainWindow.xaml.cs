﻿using Microsoft.Win32;
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
        private List<string> messageInputs = new List<string>();
        private List<string> messageOutputs = new List<string>();

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
                bool isSmsSanitised = false; // Flag to determine if the SMS message is sanitised or not

                if(!sms.sender.StartsWith("+") && !message.isIdValid() || !message.isBodyValid()) {
                    isSmsSanitised = false;
                    MessageBox.Show("Invalid entries. Please re-enter");
                }

                else if(message.isIdValid() && message.isBodyValid() && sms.sender.StartsWith("+")) // If the message ID is valid and the message body is valid
                {
                    char splitToken = ' '; // Space character to split the data
                    int smsIndex = 0;

                    string smsID = sms.messageID; // The SMS ID
                    string msgID = message.messageID;
                    smsID = msgID;

                    string smsBody = sms.messageBody;
                    string messageBody = message.messageBody;
                    smsBody = messageBody;

                    string smsCountryCode = messageBody.Split(splitToken)[smsIndex]; // Split the country code.
                    string smsSender = messageBody.Split(splitToken)[smsIndex + 1];

                    int smsIndexToProcess = smsBody.IndexOf(" ") + 1;
                    string processedSMS = smsBody.Substring(smsIndexToProcess);
                    int nextIndex = processedSMS.IndexOf(" ") + 1;

                    string finalSms = processedSMS.Substring(nextIndex);
                    sms.smsText = finalSms;

                    string newSentence = abbreviations.replaceMessage(sms.smsText);
                    sms.smsText = newSentence;

                    isSmsSanitised = true;
                    abbreviations.readFile();
                
                    if(isSmsSanitised)
                    {
                        messageID.Text = "Message ID : " + smsID.ToString();
                        messageSender.Text = "Message Sender : " + smsCountryCode.ToString() + splitToken + smsSender;
                        messageText.Text = "Message Text : " + sms.smsText.ToString();
                    }
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