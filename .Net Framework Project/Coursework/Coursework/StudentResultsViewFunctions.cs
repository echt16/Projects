using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentResultsViewFunctions : StudentViewFunctions
    {
        private StudentResultsArrayConversion StudentResultsArrayConversion { get; set; }
        public StudentResultsViewFunctions(int studentId) : base(studentId) { StudentResultsArrayConversion = new StudentResultsArrayConversion(); }
        internal List<StudentResultView> GetResultsByStudentId()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return StudentResultsArrayConversion.GetResultsForResultsView(db.GetResultsByStudentId(StudentId));
            }
        }
    }
}
