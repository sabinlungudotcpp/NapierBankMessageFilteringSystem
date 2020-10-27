using NapierBankMessageFilteringSystem.BusinessLayer;
using NapierBankMessageFilteringSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NapierBankMessageFilteringSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Abbreviations abbreviations = new Abbreviations();
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

        private void processMsgButton_Click(object sender, RoutedEventArgs e)
        {
            if (msgHeaderTxtBox.Text.Equals("") && msgTextBox.Text.Equals(""))
            {
                MessageBox.Show("Message must have a valid Sender & message body");
            }
        }

        private void readFileBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        


    }
}
