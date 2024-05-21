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

namespace project
{
    /// <summary>
    /// Логика взаимодействия для WindowType.xaml
    /// </summary>
    public partial class WindowType : Window
    {
        List<RadioButton> rb;

        public WindowType(List<RadioButton> radioButtons, bool istype)
        {
            InitializeComponent();
            rb = radioButtons;

            RList.ItemsSource = rb.Select(i => i.Content);

            if (!istype) RLabel.Content = "Кухни:";
        }

        public List<RadioButton> GetRB ()
        {
            return rb;
        }

        private void RButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (rb.Select(i => i.Content.ToString()).Contains(RTextBox.Text)) MessageBox.Show("Уже существует");
            else if (RTextBox.Text == "") MessageBox.Show("Нельзя добавлять пустой текст");
            else
            {
                rb.Add(new RadioButton { Content = RTextBox.Text, Margin = new Thickness(5), FontSize = 17 });
                RList.ItemsSource = rb.Select(i => i.Content);
            }
        }

        private void RButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if (rb.Select(i => i.Content.ToString()).Contains(RTextBox.Text)) MessageBox.Show("Уже существует");
            else if (RList.SelectedIndex < 0) MessageBox.Show("Выберите элемент");
            else if (RTextBox.Text == "") MessageBox.Show("Нельзя добавлять пустой текст");
            else if (RList.SelectedItem.ToString() == "По умолчанию" || RList.SelectedItem.ToString() == "Другое") MessageBox.Show("Нельзя менять");
            else
            {
                rb.Find(i => i.Content.ToString() == RList.SelectedItem.ToString()).Content = RTextBox.Text;
                RList.ItemsSource = rb.Select(i => i.Content);
            }
        }

        private void RButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (RList.SelectedIndex < 0) MessageBox.Show("Выберите элемент");
            else if (RList.SelectedItem.ToString() == "По умолчанию" || RList.SelectedItem.ToString() == "Другое") MessageBox.Show("Нельзя удалить");
            else
            {
                rb.RemoveAt(RList.SelectedIndex);
                RList.ItemsSource = rb.Select(i => i.Content);
            }
        }

        private void RList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RList.SelectedIndex > -1) Choosed.Content = "Выбрано: \n" + RList.SelectedItem.ToString();
        }
    }
}
