﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace YoungEnterprise_Admin_Application.UserControls
{
    /// <summary>
    /// Interaction logic for InviteUserControl.xaml
    /// </summary>
    public partial class InviteUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // The test we will be doing for this one is just to check if the user has been added to the database when time comes to that.
        // It is not worth to make a test to check if an email has been send, as it would require us to send the
        // email async just to run the test (which is not what we want to do)

        #region expand/collapse name/email/isJudge variables used for databinding
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private bool isSchool = true;
        public bool IsSchool
        {
            get { return isSchool; }
            set { isSchool = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsSchool"));
            }
        }
        #endregion

        private EmailSender mailSender = null;

        public InviteUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        // A simple method that should run on click of the Send Invite button
        private void SendEmail (object sender, RoutedEventArgs e)
        {
            // DO NOT DELETE COMMENTS BELOW THIS COMMENT:
            // gmail smtp server: smtp.gmail.com
            // port: 587
            // ssl: enable SSL
            // user: youngenterprise.mail1379@gmail.com
            // pass: yprise987
            mailSender = new EmailSender("smtp.gmail.com", 587, true, "youngenterprise.mail1379@gmail.com", "yprise987");

            Console.WriteLine(email + "  " + name);

            if (isSchool)
            {
                SendSchoolEmail();
            } else
            {
                SendJudgeEmail();
            }

            Console.WriteLine("TEST PASSED!");
        }

        // A method to send a judge email (as the content of the mail needs to be different from the school email)
        private void SendJudgeEmail ()
        {
            mailSender.SendMail(email, "Young Enterprise | Dommer Invitiation", "Hej " + name + "! Du er hermed inviteret til at blive dommer!");
            mailSender = null;
        }

        // A method to send a school email (as the content of the mail needs to be different from the judge email)
        private void SendSchoolEmail()
        {
            mailSender.SendMail(email, "Young Enterprise | Skole Invitiation", "Hej " + name + "! Du er hermed inviteret til at tilføje dine teams til eventet!");
            mailSender = null;
        }
    }
}
