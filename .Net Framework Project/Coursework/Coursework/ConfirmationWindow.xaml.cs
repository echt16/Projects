using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        private byte[] diploma;
        private byte[] passport;
        private byte[] work;
        public ConfirmationWindow()
        {
            InitializeComponent();
        }
        private void Diploma_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                diploma = File.ReadAllBytes(ofd.FileName);
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = new MemoryStream(diploma);
                bmi.EndInit();
                Diploma.Source = bmi;
            }
        }
        private void Passport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                passport = File.ReadAllBytes(ofd.FileName);
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = new MemoryStream(passport);
                bmi.EndInit();
                Passport.Source = bmi;
            }
        }
        private void Work_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                work = File.ReadAllBytes(ofd.FileName);
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = new MemoryStream(work);
                bmi.EndInit();
                FromWork.Source = bmi;
            }
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Exists())
            {
                DialogResult = true;
                this.Close();
            }
            else
                MessageBox.Show("Photos not entered!", "Error!");
        }
        public bool Exists()
        {
            if(diploma == null || passport == null || work == null)
                return false;
            return true;
        }
        public bool GetResults(out byte[] diploma, out byte[] passport, out byte[] work)
        {
            diploma = this.diploma; passport = this.passport; work = this.work;
            if (Exists())
            {
                return true;
            }
            return false;
        }
    }
}
