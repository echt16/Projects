using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForStudents.xaml
    /// </summary>
    public partial class RegistrationForStudents : Window
    {
        internal RegistrationFunctions RegistrationFunctions { get; set; }
        private Student Student { get; set; }
        internal RegistrationForStudents(RegistrationFunctions registrationFunctions) 
        {
            InitializeComponent();
            RegistrationFunctions = new RegistrationFunctions(registrationFunctions.Login, registrationFunctions.Password);
            Student = new Student();
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
        private void Name_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Name_TextBox, "Enter name...");
        }
        private void Surname_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckTextNull(Surname_TextBox, "Enter surname...");
        }
        private void Name_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Name_TextBox, "Enter name...");
        }
        private void Surname_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CheckTextEnter(Surname_TextBox, "Enter surname...");
        }
        //
        //Hundlers for button clicks
        //
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Name_TextBox.Text == "Enter surname..." || Surname_TextBox.Text == "" || Name_TextBox.Text == "Enter name..." || Surname_TextBox.Text == "")
            {
                MessageBox.Show("Error! Some data fields are not filled in!", "Field Error!");
            }
            else
            {
                RegistrationFunctions.Name = Name_TextBox.Text;
                RegistrationFunctions.Surname = Surname_TextBox.Text;
                Student.Id = RegistrationFunctions.InsertIntoTableStudents();
                StudentWindow sw = new StudentWindow(Student);
                sw.Show();
                this.Close();
            }
        }
    }
}
