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
    /// Логика взаимодействия для Changing.xaml
    /// </summary>
    public partial class Changing : Window
    {
        Dish dish;
        List<string> list;
        string n;
        public Changing(List<string> dishes, Dish dish, List<string> types, List<string> cuisines)
        {
            InitializeComponent();
            type.ItemsSource = types;
            cuisine.ItemsSource = cuisines;
            this.dish = dish;
            n = dish.Name;
            list = dishes;
            name.Text = dish.Name;
            type.SelectedIndex = type.Items.IndexOf(dish.Type);
            cuisine.SelectedIndex = cuisine.Items.IndexOf(dish.Kitchen);
            ing.Text = "";
            ins.Text = "";
            cuisine.Text = dish.Kitchen;
            type.Text = dish.Type;
            foreach(var item in dish.Ingredients)
            {
                ing.Text += $"{item}\n";
            }
            foreach(var item in dish.Instruction)
            {
                ins.Text += $"{item}\n";
            }
        }

        public Dish GetDish()
        {
            if(name.Text != "")
                dish.Name = name.Text;
            if(cuisine.Text != "")
                dish.Kitchen = cuisine.Text;
            if(type.Text != "")
                dish.Type = type.Text;
            if(ins.Text != "")
                dish.Instruction = ins.Text.Split('\n').ToList();
            if(ing.Text != "")
                dish.Ingredients = ing.Text.Split('\n').ToList();
            return dish;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Ok")
            {
                if (list.Contains(name.Text) && name.Text != n)
                {
                    MessageBox.Show("Блюдо уже присутствует в списке рецептов", "Измените именование");
                }
                else if (type.Text == "" || cuisine.Text == "" || ing.Text == "" || ins.Text == "")
                {
                    MessageBox.Show("Введите все данные!");
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
            if (ofd.ShowDialog() == true)
            {
                Uri uri = new Uri(ofd.FileName);
                dish.Image.Source = new BitmapImage(uri);
            }
        }
    }
}
