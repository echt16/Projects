using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coursework
{
    internal class StudentFunctions : IStudentFunctions
    {
        internal int StudentId { get; set; }
        private StudentTestsViewFunctions TestsViewFunctions { get; set; }
        private StudentResultsViewFunctions ResultsViewFunctions { get; set; }
        private StudentProfilesViewFunctions ProfilesViewFunctions { get; set; }
        internal StudentFunctions() { }
        internal StudentFunctions(int studentId)
        {
            StudentId = studentId;
            TestsViewFunctions = new StudentTestsViewFunctions(StudentId);
            ResultsViewFunctions = new StudentResultsViewFunctions(StudentId);
            ProfilesViewFunctions = new StudentProfilesViewFunctions(StudentId);
        }
        internal int CountOfTestsForStudent()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return db.TestsStudents.ToList().Where(x => x.StudentId == StudentId).Count();
            }
        }
        internal int CountOfTestsForGroup()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return db.TestsGroups.ToList().Join(db.Students.ToList(), x => x.GroupId, y => y.GroupId, (x, y) => new { y.Id }).Where(x => x.Id == StudentId).Count();
            }
        }
        internal Student GetCurrentStudent()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return db.Students.SingleOrDefault(x => x.Id == StudentId);
            }
        }
        internal string GetLoginOfCurrentStudent()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                long loginPasswordId = db.StudentsAccounts.First(x => x.StudentId == StudentId).LoginPasswordId;
                return db.LoginsPasswords.First(x => x.Id == loginPasswordId).Login;
            }
        }
        internal string GetPasswordOfCurrentStudent()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                long loginPasswordId = db.StudentsAccounts.First(x => x.StudentId == StudentId).LoginPasswordId;
                return db.LoginsPasswords.First(x => x.Id == loginPasswordId).Password;
            }
        }
        internal void ChangeNameOfCurrentStudent(string newName)
        {
            lock (this)
            {
                using (TestApplicationDBContext db = new TestApplicationDBContext())
                {
                    db.Students.First(x => x.Id == StudentId).Name = newName;
                    db.SaveChanges();
                }
            }
        }
        internal void ChangeSurnameOfCurrentStudent(string newSurname)
        {
            lock (this)
            {
                using (TestApplicationDBContext db = new TestApplicationDBContext())
                {
                    db.Students.First(x => x.Id == StudentId).Surname = newSurname;
                    db.SaveChanges();
                }
            }
        }
        internal void ChangeLoginOfCurrentStudent(string newLogin)
        {
            lock (this)
            {
                using (TestApplicationDBContext db = new TestApplicationDBContext())
                {
                    long loginPasswordId = db.StudentsAccounts.First(x => x.StudentId == StudentId).LoginPasswordId;
                    db.LoginsPasswords.First(x => x.Id == loginPasswordId).Login = newLogin;
                    db.SaveChanges();
                }
            }
        }
        internal void ChangePasswordOfCurrentStudent(string newPassword)
        {
            lock (this)
            {
                using (TestApplicationDBContext db = new TestApplicationDBContext())
                {
                    long loginPasswordId = db.StudentsAccounts.First(x => x.StudentId == StudentId).LoginPasswordId;
                    db.LoginsPasswords.First(x => x.Id == loginPasswordId).Password = newPassword;
                    db.SaveChanges();
                }
            }
        }
        internal void DeleteAccountOfCurrentStudent()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                db.DeleteStudentAccount(StudentId);
            }
        }
        internal List<StudentTestView> GetTests()
        {
            return TestsViewFunctions.GetTestsByStudentId();
        }
        internal List<StudentTestView> GetTestsForGroup()
        {
            return TestsViewFunctions.GetTestsForGroupByStudentId();
        }
        internal List<StudentResultView> GetResults()
        {
            return ResultsViewFunctions.GetResultsByStudentId();
        }
        internal List<StudentTestView> GetTestsBySubjectId(short subjectId)
        {
            return TestsViewFunctions.GetTestBySubjectId(subjectId);
        }
        internal StudentProfileView GetProfile()
        {
            return ProfilesViewFunctions.GetProfileByStudentId();
        }
        internal List<StudentTestView> GetTestsByRequest(string request)
        {
            return TestsViewFunctions.GetTestsByRequest(request);
        }
    }
}
