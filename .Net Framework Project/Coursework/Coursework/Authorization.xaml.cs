using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private AuthorizationFunctions AuthorizationFunctions { get; set; }
        public Authorization()
        {
            InitializeComponent();
            AuthorizationFunctions = new AuthorizationFunctions();
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
        //Focus handler for textboxes
        //
        private void Login_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Login_TextBox, "Enter login...");
        }
        private void Password_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Password_TextBox, "Enter password...");
        }
        private void Login_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Login_TextBox, "Enter login...");
        }
        private void Password_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Password_TextBox, "Enter password...");
        }
        //
        //Function to chek user type
        //
        private int WhatType()
        {
            if (Student_RadioButton.IsChecked == true)
            {
                return 0;
            }
            if (Teacher_RadioButton.IsChecked == true)
            {
                return 1;
            }
            if (Admin_RadioButton.IsChecked == true)
            {
                return 2;
            }
            return -1;
        }
        //
        //Hundlers for button clicks
        //
        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            int id;
            AuthorizationFunctions.Type = WhatType();
            AuthorizationFunctions.Login = Login_TextBox.Text;
            AuthorizationFunctions.Password = Password_TextBox.Text;
            if (!AuthorizationFunctions.CheckAuthorization(out id))
            {
                MessageBox.Show("Error! Some data was entered incorectly!", "Error!");
                return;
            }
            else
            {
                if (AuthorizationFunctions.Type == 0)
                {
                    Student student = new Student() { Id = id };
                    StudentWindow sw = new StudentWindow(student);
                    sw.Show();
                }
                else if (AuthorizationFunctions.Type == 1)
                {
                    Teacher teacher = new Teacher() { Id = id };
                    TeacherWindow tw = new TeacherWindow(teacher);
                    tw.Show();
                }
                else if (AuthorizationFunctions.Type == 2)
                {
                    Administrator administrator = new Administrator() { Id = id };
                    AdminWindow aw = new AdminWindow(administrator);
                    aw.Show();
                }
                this.Close();
                return;
            }
        }
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
