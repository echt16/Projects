using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentViewFunctions : IStudentViewFunctions
    {
        public int StudentId {  get; set; }
        public StudentViewFunctions(int studentId) { StudentId = studentId; }   
        public StudentViewFunctions() {  }   
    }
}
