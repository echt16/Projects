using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentProfilesViewFunctions : StudentViewFunctions
    {
        private StudentProfilesArrayConversion StudentProfilesArrayConversion { get; set; }
        public StudentProfilesViewFunctions(int studentId) : base(studentId) { StudentProfilesArrayConversion = new StudentProfilesArrayConversion(); }
        internal StudentProfileView GetProfileByStudentId()
        {
            using(TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return (StudentProfilesArrayConversion.GetProfiles(db.GetInfoAboutStudentProfile(StudentId))[0]);
            }
        }
    }
}
