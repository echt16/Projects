using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
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

namespace project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dish> dishes = new List<Dish>();

        string checkedType = "";
        string checkedCuisine = "";

        List<RadioButton> RadioButtonsTypes = new List<RadioButton>();
        List<RadioButton> RadioButtonsCuisines= new List<RadioButton>();

        public MainWindow()
        {
            InitializeComponent();
            RadioButtonsTypes.Add(TypeDefault);
            RadioButtonsTypes.Add(TypeSalat);
            RadioButtonsTypes.Add(TypeDesert);
            RadioButtonsTypes.Add(TypeBakery);
            RadioButtonsTypes.Add(TypeOther);

            RadioButtonsCuisines.Add(CuisineDefault);
            RadioButtonsCuisines.Add(CuisineItaly);
            RadioButtonsCuisines.Add(CuisineJapan);
            RadioButtonsCuisines.Add(CuisineEuropean);
            RadioButtonsCuisines.Add(CuisineOther);

            Dish d1 = new Dish();
            d1.Name = "Бутерброд с колбасой";
            d1.Type = "Другое";
            d1.Kitchen = "Другое";
            List<string> i1 = new List<string>();
            i1.Add("Хлеб");
            i1.Add("Колбаса");
            List<string> ins1 = new List<string>();
            ins1.Add("Отрезать кусок хлеба");
            ins1.Add("Отрезать кусок колбасы");
            ins1.Add("Положить колбасу на хлеб");
            d1.Ingredients = i1;
            d1.Instruction = ins1;

            dishes.Add(d1);

            ListDishes.ItemsSource = dishes.Select(i => i.Name);
        }

        private void TypeDefault_Checked(object sender, RoutedEventArgs e)
        {
            checkedType = "";
            if (checkedCuisine == "") ListDishes.ItemsSource = dishes.Select(i => i.Name);
            else ListDishes.ItemsSource = dishes.Where(x => x.Kitchen == checkedCuisine).Select(i => i.Name);
        }

        private void Type_Checked(object sender, RoutedEventArgs e)
        {
            checkedType = (sender as RadioButton).Content.ToString();
            if (checkedCuisine == "") ListDishes.ItemsSource = dishes.Where(x => x.Type == checkedType).Select(i => i.Name);
            else ListDishes.ItemsSource = dishes.Where(x => x.Kitchen == checkedCuisine).Where(x => x.Type == checkedType).Select(i => i.Name);
        }


        private void CuisineDefault_Checked(object sender, RoutedEventArgs e)
        {
            checkedCuisine = "";
            if (checkedType == "") ListDishes.ItemsSource = dishes.Select(i => i.Name);
            else ListDishes.ItemsSource = dishes.Where(x => x.Type == checkedType).Select(i => i.Name);
        }

        private void Cuisine_Checked(object sender, RoutedEventArgs e)
        {
            checkedCuisine = (sender as RadioButton).Content.ToString();
            if (checkedType == "") ListDishes.ItemsSource = dishes.Where(x => x.Kitchen == checkedCuisine).Select(i => i.Name);
            else ListDishes.ItemsSource = dishes.Where(x => x.Type == checkedType).Where(x => x.Kitchen == checkedCuisine).Select(i => i.Name);
        }

        private void ListDishes_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (ListDishes.SelectedItem != null)
            {
                Window1 window1 = new Window1(dishes.Find(x => x.Name == ListDishes.SelectedItem.ToString()));
                window1.Show();
            }
        }

        private void ListDishes_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListDishes.SelectedItem != null)
            {
                Dish td = dishes.Find(x => x.Name == ListDishes.SelectedItem.ToString());
                MessageBox.Show("Тип блюда: " + td.Type + "\nКухня: " + td.Kitchen, "О блюде");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Adding adding = new Adding(dishes.Select(x => x.Name).ToList(),
                RadioButtonsTypes.Select(i => i.Content.ToString()).Where(i => i != "По умолчанию").ToList(),
                RadioButtonsCuisines.Select(i => i.Content.ToString()).Where(i => i != "По умолчанию").ToList());
            if(adding.ShowDialog() == true)
            {
                dishes.Add(adding.GetDish());

                Sort();
            }

        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if (ListDishes.SelectedIndex < 0) MessageBox.Show("Выберите элемент");
            else
            {
                Changing changing = new Changing(dishes.Select(x => x.Name).ToList(), dishes[ListDishes.SelectedIndex],
                    RadioButtonsTypes.Select(i => i.Content.ToString()).Where(i => i != "По умолчанию").ToList(),
                    RadioButtonsCuisines.Select(i => i.Content.ToString()).Where(i => i != "По умолчанию").ToList());
                if (changing.ShowDialog() == true)
                {
                    dishes[ListDishes.SelectedIndex] = changing.GetDish();

                    Sort();
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListDishes.SelectedIndex < 0) MessageBox.Show("Выберите элемент");
            else
            {
                dishes.RemoveAt(dishes.FindIndex(i => i.Name == ListDishes.SelectedItem.ToString()));


                Sort();
            }
        }

        private void ButtonExport_Click(object sender, RoutedEventArgs e)
        {
            if (ListDishes.SelectedIndex < 0) MessageBox.Show("Выберите элемент");
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "txt files (*.txt) | *.txt | doc files (*.doc) | *.doc";

                if (save.ShowDialog().Value == true)
                {
                    FileStream fs = (FileStream)save.OpenFile();
                    AddText(fs, dishes.Find(i => i.Name == ListDishes.SelectedItem.ToString()).ToString());
                    fs.Close();
                }
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        public void Sort()
        {
            if (checkedCuisine == "" && checkedType == "") ListDishes.ItemsSource = dishes.Select(i => i.Name);
            else if (checkedCuisine == "") ListDishes.ItemsSource = dishes.Where(x => x.Type == checkedType).Select(i => i.Name);
            else if (checkedType == "") ListDishes.ItemsSource = dishes.Where(x => x.Kitchen == checkedCuisine).Select(i => i.Name);
            else ListDishes.ItemsSource = dishes.Where(x => x.Type == checkedType).Where(x => x.Kitchen == checkedCuisine).Select(i => i.Name);
        }

        private void ButtonType_Click(object sender, RoutedEventArgs e)
        {
            WindowType type = new WindowType(RadioButtonsTypes, true);
            type.ShowDialog();
            DockType.Children.Clear();
            foreach (RadioButton r in type.GetRB())
            {
                DockType.Children.Add(r);
                DockPanel.SetDock(r, Dock.Top);
            }

            foreach (RadioButton r in RadioButtonsTypes) r.Checked += Type_Checked;
            RadioButtonsTypes.Find(i => i.Content.ToString() == "По умолчанию").Checked += TypeDefault_Checked;
            RadioButtonsTypes.Find(i => i.Content.ToString() == "По умолчанию").IsChecked = true;

            foreach(Dish d in dishes)
            {
                if (!RadioButtonsTypes.Select(i => i.Content.ToString()).Contains(d.Type))
                {
                    d.Type = "Другое";
                }
            }
        }

        private void ButtonCuisine_Click(object sender, RoutedEventArgs e)
        {
            WindowType type = new WindowType(RadioButtonsCuisines, false);
            type.ShowDialog();
            DockCuisine.Children.Clear();
            foreach (RadioButton r in type.GetRB())
            {
                DockCuisine.Children.Add(r);
                DockPanel.SetDock(r, Dock.Top);
            }

            foreach (RadioButton r in RadioButtonsCuisines) r.Checked += Cuisine_Checked;
            RadioButtonsCuisines.Find(i => i.Content.ToString() == "По умолчанию").Checked += CuisineDefault_Checked;
            RadioButtonsCuisines.Find(i => i.Content.ToString() == "По умолчанию").IsChecked = true;

            foreach (Dish d in dishes)
            {
                if (!RadioButtonsCuisines.Select(i => i.Content.ToString()).Contains(d.Kitchen))
                {
                    d.Kitchen = "Другое";
                }
            }
        }
    }

    public class Dish
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Kitchen { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instruction { get; set; }
        public Image Image { get; set; }
        public Dish()
        {
            Name = "";
            Type = "";
            Kitchen = "";
            Ingredients = new List<string>();
            Instruction = new List<string>();
            Image = new Image();

        }
        public override string ToString()
        {
            string alltext = "";

            alltext += "Блюдо: " + Name + "\n";
            alltext += "Тип блюда: " + Type + "\n";
            alltext += "Кухня: " + Kitchen + "\n\n";

            alltext += "Инградиенты:\n";
            foreach (string s in Ingredients) alltext += s + "\n";

            alltext += "\nРецепт:\n";
            for (int i = 0; i < Instruction.Count; i++) alltext += (i + 1).ToString() + ". " + Instruction[i] + "\n";

            return alltext;
        }
    }
}
