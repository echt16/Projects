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
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private RegistrationFunctions RegistrationFunctions { get; set; }
        public Registration()
        {
            InitializeComponent();
            Login_TextBox.Foreground = Brushes.Black;
            Password_TextBox.Foreground = Brushes.Black;
            Password2_TextBox.Foreground = Brushes.Black;
            RegistrationFunctions = new RegistrationFunctions();
        }
        //
        //Functions for checking data in textboxes
        //
        public void CheckTextNull(TextBox textBox, string text)
        {
            if (textBox.Text == "")
            {
                textBox.Foreground = Brushes.Gray;
                textBox.Text = text;
            }
        }
        public void CheckTextEnter(TextBox textBox, string text)
        {
            if (textBox.Text == text)
            {
                textBox.Foreground = Brushes.Black;
                textBox.Text = "";
            }
        }
        //
        //Focus handlers for textboxes
        //
        private void Login_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Login_TextBox, "Enter login...");
        }
        private void Password_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Password_TextBox, "Enter password...");
        }
        private void Password2_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Password2_TextBox, "Enter password again...");
        }
        private void Login_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Login_TextBox, "Enter login...");
        }
        private void Password_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Password_TextBox, "Enter password...");
        }
        private void Password2_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Password2_TextBox, "Enter password again...");
        }
        //
        //Secondary functions
        //
        private bool IsChakedRadioButtons()
        {
            if (radioButton1.IsChecked != true && radioButton2.IsChecked != true)
            {
                return false;
            }
            return true;
        }
        private int WhatType()
        {
            if (radioButton1.IsChecked == true)
                return 0;
            else if (radioButton2.IsChecked == true)
                return 1;
            return -1;
        }
        private bool CheckEnteredData()
        {
            if (Password_TextBox.Text == "Enter password..." || Password_TextBox.Text == "" || Login_TextBox.Text == "Enter login..." || Login_TextBox.Text == "" || !IsChakedRadioButtons())
            {
                return false;
            }
            return true;
        }
        private bool CheckPasswords()
        {
            if(Password_TextBox.Text != Password2_TextBox.Text)
                return false;
            return true;
        }
        private void NextWindow()
        {
            if (RegistrationFunctions.Type == 0)
            {
                RegistrationForStudents students = new RegistrationForStudents(RegistrationFunctions);
                students.Show();
            }
            else
            {
                RegistrationForTeachers teachers = new RegistrationForTeachers(RegistrationFunctions);
                teachers.Show();
            }
            this.Close();
        }
        //
        //Hundlers for button clicks
        //
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            RegistrationFunctions.Login = Login_TextBox.Text;
            RegistrationFunctions.Password = Password_TextBox.Text;
            RegistrationFunctions.Type = WhatType();
            if(RegistrationFunctions.CheckLoginOfCurrentUser())
            {
                MessageBox.Show("This login already esists");
                return;
            }
            if(CheckEnteredData() && CheckPasswords())
            {
                NextWindow();
            }
            else
            {
                MessageBox.Show("Error! Some data fields are not filled in!", "Field Error!");
            }
        }
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
