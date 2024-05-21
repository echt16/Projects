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
using Microsoft.Win32;

namespace project
{
    /// <summary>
    /// Логика взаимодействия для Adding.xaml
    /// </summary>
    public partial class Adding : Window
    {
        Dish dish;
        List<string> list;
        public Adding(List<string> dishes, List<string> types, List<string> cuisines)
        {
            InitializeComponent();
            dish = new Dish();
            list = dishes;
            type.ItemsSource = types;
            cuisine.ItemsSource = cuisines;
        }

        public Dish GetDish()
        {
            dish.Name = name.Text;
            dish.Kitchen = cuisine.Text;
            dish.Type = type.Text;
            dish.Instruction = ins.Text.Split('\n').ToList();
            dish.Ingredients = ing.Text.Split('\n').ToList();
            return dish;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Content.ToString() == "Ok")
            {
                if(type.Text == "" || cuisine.Text == "" || ing.Text == "" || ins.Text == "")
                {
                    MessageBox.Show("Введите все данные!");
                }
                else if (list.Contains(name.Text))
                {
                    MessageBox.Show("Блюдо уже присутствует в списке рецептов", "Измените именование");
                }
                else
                {
                    DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                DialogResult = false;   
                this.Close();
            }
        }

        private void AddFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png";
            if(ofd.ShowDialog() == true)
            {
                Uri uri = new Uri(ofd.FileName);
                dish.Image.Source = new BitmapImage(uri);
            }
        }
    }
}
