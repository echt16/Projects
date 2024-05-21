using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentTestSingleConversion : IStudentSingleConversion
    {
        internal static StudentTestView GetTest(GetTestsForStudent_Result result)
        {
            return GetTest(result.TestName, result.TestId, result.TestDeadLine, result.TeacherName);
        }
        internal static StudentTestView GetTest(GetTestsForGroup_Result result)
        {
            return GetTest(result.TestName, result.TestId, result.TestDeadLine, result.TeacherName);
        }
        internal static StudentTestView GetTest(GetAllAvailibleTestsBySubject_Result result)
        {
            return GetTest(result.TestName, result.TestId, result.TestDeadLine, result.TeacherName);
        }
        internal static StudentTestView GetTest(GetTestsByRequest_Result result)
        {
            return GetTest(result.TestName, result.TestId, result.TestDeadLine, result.TeacherName);
        }
        private static StudentTestView GetTest(string name, long id, DateTime? dateTime, string teacherName)
        {
            if (dateTime != null)
                return new StudentTestView() { Name = name, Id = id, DeadLine = $"{dateTime.Value.Day}.{dateTime.Value.Month}.{dateTime.Value.Year} {dateTime.Value.Hour}:{dateTime.Value.Minute}", TeacherName = teacherName };
            else
                return new StudentTestView() { Name = name, Id = id, DeadLine = $"without limitation", TeacherName = teacherName };

        }
    }
}
