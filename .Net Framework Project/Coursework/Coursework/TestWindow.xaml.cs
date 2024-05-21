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

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private Test Test {  get; set; }
        private Student Student { get; set; }
        public TestWindow(Test test, Student student)
        {
            InitializeComponent();
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                Test = db.Tests.ToList().First(x => x.Id == test.Id);
                Student = db.Students.ToList().First(x => x.Id == student.Id);
            }
            MessageBox.Show(Test.Id.ToString());
            this.Title = Test.Name;
        }
    }
}
