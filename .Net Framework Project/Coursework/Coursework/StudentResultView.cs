using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentResultView : IStudentView
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
        public string Score { get; internal set; }
        public string Time { get; internal set; }
        public string TimeSpan { get; internal set; }
        public string TeacherName { get; internal set; }
    }
}
