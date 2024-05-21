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
using static System.Net.Mime.MediaTypeNames;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student Student { get; private set; }
        private StudentFunctions Functions { get; set; }
        internal StudentWindow(Student student)
        {
            InitializeComponent();
            Functions = new StudentFunctions(student.Id);
            Student = Functions.GetCurrentStudent();
            Greating_Label.Content = $"Hello, {Student.Name}!";
            ThreadPool.QueueUserWorkItem(CheckCounts, Student.Id);
            ThreadPool.QueueUserWorkItem(LoadProfileInfo, Student.Id);
        }
        //
        //Function to determine the number of current tests
        //
        private void CheckCounts(object obj)
        {
            int Id = (int)obj;
            lock (Functions)
            {
                TestsCount_Label.Dispatcher.Invoke(new Action(() => { TestsCount_Label.Content = Functions.CountOfTestsForStudent(); }));
                GroupsCount_Label.Dispatcher.Invoke(new Action(() => { GroupsCount_Label.Content = Functions.CountOfTestsForGroup(); }));
            }
        }
        //
        //Function to open a view
        //
        private void OpenDockPanel(DockPanel dockPanel)
        {
            MyTests_DockPanel.Visibility = Visibility.Collapsed;
            GroupsTests_DockPanel.Visibility = Visibility.Collapsed;
            Results_DockPanel.Visibility = Visibility.Collapsed;
            Profile_DockPanel.Visibility = Visibility.Collapsed;
            AllCategories_DockPanel.Visibility = Visibility.Collapsed;
            AllTests_DockPanel.Visibility = Visibility.Collapsed;
            dockPanel.Visibility = Visibility.Visible;
        }
        //
        //Clicking the main buttons
        //
        private void MyTests_Button_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadTestsForStudent, Student.Id);
            OpenDockPanel(MyTests_DockPanel);
        }
        private void GroupsTests_Button_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadTestsForGroup, Student.Id);
            OpenDockPanel(GroupsTests_DockPanel);
        }
        private void Results_Button_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadResults, Student.Id);
            OpenDockPanel(Results_DockPanel);
        }
        private void AllTests_Button_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadCategories);
            OpenDockPanel(AllCategories_DockPanel);
        }
        private void Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadProfileInfo, Student.Id);
            OpenDockPanel(Profile_DockPanel);
        }
        //
        //Loading innformation for views
        //
        private void LoadTestsForStudent(object obj)
        {
            int Id = (int)obj;
            MyTests_ListView.Dispatcher.Invoke(new Action(() => { MyTests_ListView.ItemsSource = Functions.GetTests(); }));
        }
        private void LoadTestsForGroup(object obj)
        {
            int Id = (int)obj;
            GroupsTests_ListView.Dispatcher.Invoke(new Action(() => { GroupsTests_ListView.ItemsSource = Functions.GetTestsForGroup(); }));
        }
        private void LoadResults(object obj)
        {
            int Id = (int)obj;
            Results_ListView.Dispatcher.Invoke(new Action(() => { Results_ListView.ItemsSource = Functions.GetResults(); }));
        }
        private void LoadCategories(object obj)
        {
            Categories_ListView.Dispatcher.Invoke(new Action(() => { Categories_ListView.ItemsSource = (new SubjectsViewFunctions()).GetAllSubjects(); }));
        }
        private void LoadTestsByCategory(object obj)
        {
            short Id = (short)obj;
            AllTests_ListView.Dispatcher.Invoke(new Action(() => { AllTests_ListView.ItemsSource = Functions.GetTestsBySubjectId(Id); }));
            Category_Label.Dispatcher.Invoke(new Action(() => Category_Label.Content = GeneralFunctions.GetSubjectNameById(Id)));
        }
        private void LoadProfileInfo(object obj)
        {
            int Id = (int)obj;
            StudentProfileView profile = null;
            profile = Functions.GetProfile();
            Name_TextBox.Dispatcher.Invoke(new Action(() => Name_TextBox.Text = profile.Name));
            Surname_TextBox.Dispatcher.Invoke(new Action(() => Surname_TextBox.Text = profile.Surname));
            Group_Label.Dispatcher.Invoke(new Action(() => Group_Label.Content = profile.GroupName));
            Login_TextBox.Dispatcher.Invoke(new Action(() => Login_TextBox.Text = profile.Login));
            Password_TextBox.Dispatcher.Invoke(new Action(() => Password_TextBox.Text = profile.Password));
        }
        private void LoadTestsByRequest(object obj)
        {
            string request = (string)obj;
            AllTests_ListView.Dispatcher.Invoke(new Action(() => { AllTests_ListView.ItemsSource = Functions.GetTestsByRequest(request); }));
        }
        //
        //Clicking buttons inside the views
        //
        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (Categories_ListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a category!");
                return;
            }
            short id = (Categories_ListView.Items[Categories_ListView.SelectedIndex] as dynamic).SubjectId;
            ThreadPool.QueueUserWorkItem(LoadTestsByCategory, id);
            OpenDockPanel(AllTests_DockPanel);
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OpenDockPanel(AllCategories_DockPanel);
        }
        private void TakeMyTest_Click(object sender, RoutedEventArgs e)
        {
            if (MyTests_ListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a test!");
                return;
            }
            long Id = (MyTests_ListView.Items[MyTests_ListView.SelectedIndex] as dynamic).Id;
            TestWindow testWindow = new TestWindow(new Test() { Id = Id }, new Student() { Id = Student.Id });
            testWindow.Show();
        }
        private void TakeTest_Click(object sender, RoutedEventArgs e)
        {
            if (AllTests_ListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a test!");
                return;
            }
            long Id = (AllTests_ListView.Items[AllTests_ListView.SelectedIndex] as dynamic).Id;
            TestWindow testWindow = new TestWindow(new Test() { Id = Id }, new Student() { Id = Student.Id });
            testWindow.Show();
        }
        private void TakeGroupTest_Click(object sender, RoutedEventArgs e)
        {
            if (GroupsTests_ListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a test!");
                return;
            }
            long Id = (GroupsTests_ListView.Items[GroupsTests_ListView.SelectedIndex] as dynamic).Id;
            TestWindow testWindow = new TestWindow(new Test() { Id = Id }, new Student() { Id = Student.Id });
            testWindow.Show();
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadTestsByRequest, SearchRequest_TextBox.Text);
        }
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(DeleteAccount);
            this.Close();
        }
        //
        //Double clicking on buttons inside profile view
        //
        private void Name_TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Name_TextBox.IsReadOnly = false;
        }
        private void Surname_TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Surname_TextBox.IsReadOnly = false;
        }
        private void Login_TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Login_TextBox.IsReadOnly = false;
        }
        private void Password_TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Password_TextBox.IsReadOnly = false;
        }
        //
        //Handlers for losing focus of textboxes inside a profile view
        //
        private void Name_TextBox_LostFokus(object sender, RoutedEventArgs e)
        {
            Name_TextBox.IsReadOnly = true;
            if (Name_TextBox.Text.Length == 0)
            {
                Name_TextBox.Text = Student.Name;
                return;
            }
            Student.Name = Name_TextBox.Text;
            Greating_Label.Content = $"Hello, {Student.Name}!";
            ThreadPool.QueueUserWorkItem(ChangeName, Student);
        }
        private void Surname_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Surname_TextBox.IsReadOnly = true;
            if (Surname_TextBox.Text.Length == 0)
            {
                Surname_TextBox.Text = Student.Surname;
                return;
            }
            Student.Surname = Surname_TextBox.Text;
            ThreadPool.QueueUserWorkItem(ChangeSurname, Student);
        }
        private void Login_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Login_TextBox.IsReadOnly = true;
            if (Login_TextBox.Text == Functions.GetLoginOfCurrentStudent())
                return;
            if (RegistrationFunctions.CheckLogin(Login_TextBox.Text))
            {
                MessageBox.Show("Login already exists!");
                return;
            }
            if (Login_TextBox.Text.Length == 0)
            {
                Login_TextBox.Text = Functions.GetLoginOfCurrentStudent();
                return;
            }
            ThreadPool.QueueUserWorkItem(ChangeLogin, Login_TextBox.Text);
        }
        private void Password_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Password_TextBox.IsReadOnly = true;
            if (Password_TextBox.Text.Length == 0)
            {
                Password_TextBox.Text = Functions.GetPasswordOfCurrentStudent();
                return;
            }
            ThreadPool.QueueUserWorkItem(ChangePassword, Password_TextBox.Text);
        }
        //
        //Changing information in the database about profile
        //
        private void ChangeName(object obj)
        {
            string name = (obj as Student).Name;
            Functions.ChangeNameOfCurrentStudent(name);
        }
        private void ChangeSurname(object obj)
        {
            string surname = (obj as Student).Surname;
            Functions.ChangeSurnameOfCurrentStudent(surname);
        }
        private void ChangeLogin(object obj)
        {
            string login = (obj.ToString());
            Functions.ChangeLoginOfCurrentStudent(login);
        }
        private void ChangePassword(object obj)
        {
            string password = obj.ToString();
            Functions.ChangePasswordOfCurrentStudent(password);
        }
        private void DeleteAccount(object obj)
        {
            Functions.DeleteAccountOfCurrentStudent();
        }
        //
        //Clearing of SearchRequest_TextBox by click
        //
        private void SearchRequest_TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchRequest_TextBox.Text = string.Empty;
        }
    }
}
