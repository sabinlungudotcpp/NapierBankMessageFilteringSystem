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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Abbreviations abbreviations = new Abbreviations();
        private Message message = new Message(); // New message instance

        public Sms sms = new Sms(); // New SMS instance
        public Email email = new Email();
        public Tweet tweets = new Tweet();

        private List<string> incidentList = new List<string>();
        private List<string> quarantineList = new List<string>(); // Declares a new list to store the URLs that are quarantined
        private List<string> mentionsList = new List<string>();

       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void processMsgButton_Click(object sender, RoutedEventArgs e) // Process Message ID & Body manually
        {
            try
            {

            } 
            
            catch
            {
                throw new Exception("An error has occurred");
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
