using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentProfileView : IStudentView
    {
        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public string GroupName { get; internal set; }
        public string Login { get; internal set; }
        public string Password { get; internal set; }
    }
}
