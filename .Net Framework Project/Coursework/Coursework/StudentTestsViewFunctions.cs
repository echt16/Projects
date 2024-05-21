using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentTestsViewFunctions : StudentViewFunctions
    {
        private StudentTestsArrayConversion StudentTestsArrayConversion { get; set; }
        public StudentTestsViewFunctions(int studentId) : base(studentId) { StudentTestsArrayConversion = new StudentTestsArrayConversion(); }
        internal List<StudentTestView> GetTestsByStudentId()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return StudentTestsArrayConversion.GetTestsForTestsView(db.GetTestsForStudent(StudentId));
            }
        }
        internal List<StudentTestView> GetTestsForGroupByStudentId()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return StudentTestsArrayConversion.GetTestsForTestsView(db.GetTestsForGroup(StudentId));
            }
        }
        internal List<StudentTestView> GetTestsByRequest(string request)
        {
            using(TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return StudentTestsArrayConversion.GetTestsForTestsView(db.GetTestsByRequest(request));
            }
        }
        internal List<StudentTestView> GetTestBySubjectId(short subjectId)
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return StudentTestsArrayConversion.GetTestsForTestsView(db.GetAllAvailibleTestsBySubject(subjectId));
            }
        }
    }
}
