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

namespace Project_Client_
{
    public partial class ChoosePlayer : Window
    {
        public bool is1;
        public bool choosed = false;
        public ChoosePlayer()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            choosed = true;
            is1 = true;
            Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            choosed = true;
            is1 &= false;
            Close();
        }
    }
}
