using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentTestView : IStudentView
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
        public string DeadLine { get; internal set; }
        public string TeacherName { get; internal set; }
    }
}
