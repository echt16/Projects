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
    /// Логика взаимодействия для RegistrationForTeacher.xaml
    /// </summary>
    public partial class RegistrationForTeachers : Window
    {
        internal RegistrationFunctions RegistrationFunctions { get; set; }
        public string EducationalEstablishment { get; set; }
        public List<byte[]> Confirmation { get; set; }

        internal RegistrationForTeachers(RegistrationFunctions registrationFunctions)
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(FillEducationalEstablishment_ComboBox);
            RegistrationFunctions = new RegistrationFunctions(registrationFunctions.Login, registrationFunctions.Password);
            Confirmation = new List<byte[]>()
            {
                null,
                null,
                null
            };
        }
        //
        //initial entering for EducationalEstablishment_ComboBox
        //
        private void FillEducationalEstablishment_ComboBox(object obj)
        {
            List<string> res = null;
            Dispatcher.Invoke(new Action(() => { res = TeacherEducationalEstablishmentViewFunctions.GetEducationalEstablishmentForRegistrationWindow(); }));

            foreach (string item in res)
            {
                EducationalEstablishment_ComboBox.Dispatcher.Invoke(new Action(() =>
                {
                    EducationalEstablishment_ComboBox.Items.Add(item);
                }));
            }
        }
        //
        //Functions for checking entered data
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
        //Handlers for textbox events
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
            CheckTextEnter(Surname_TextBox, "Enter Surname...");
        }
        //
        //Handlers for button click
        //
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Name_TextBox.Text == "Enter Surname..." || Surname_TextBox.Text == "" || Name_TextBox.Text == "Enter name..." || Name_TextBox.Text == "" || EducationalEstablishment_ComboBox.SelectedItem == null || !RegistrationFunctions.CheckConfirmation(Confirmation))
            {
                MessageBox.Show("Error! Some data fields are not filled in!", "Field Error!");
            }
            else
            {
                RegistrationFunctions.Name = Name_TextBox.Text;
                RegistrationFunctions.Surname = Surname_TextBox.Text;
                EducationalEstablishment = EducationalEstablishment_ComboBox.SelectedItem.ToString().Split(',')[0];
                AddIntoDataBase();
                FirstTeacherWindow ftw = new FirstTeacherWindow();
                ftw.Show();
                this.Close();
            }
        }
        private void Confirmation_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationWindow confirmation = new ConfirmationWindow();
            byte[] diploma, passport, work;
            if (confirmation.ShowDialog() == true && confirmation.GetResults(out diploma, out passport, out work))
            {
                Confirmation[0] = diploma;
                Confirmation[1] = passport;
                Confirmation[2] = work;
            }
        }
        //
        //Adding into database
        //
        private void AddIntoDataBase()
        {
            RegistrationFunctions.InsertIntoTableTeachers(Confirmation, EducationalEstablishment);
        }
    }
}
