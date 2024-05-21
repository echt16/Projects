using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentProfilesArrayConversion : IStudentArrayConversion
    {
        private List<StudentProfileView> ProfileViews { get; set; }
        public StudentProfilesArrayConversion()
        {
            ProfileViews = new List<StudentProfileView>();
        }
        internal List<StudentProfileView> GetProfiles(System.Data.Entity.Core.Objects.ObjectResult<GetInfoAboutStudentProfile_Result> res)
        {
            foreach (var profile in res)
            {
                ProfileViews.Add(StudentProfileSingleConversion.GetProfile(profile));
            }
            List<StudentProfileView> profileViews = ProfileViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearProfileViews);
            return profileViews;
        }
        private void ClearProfileViews(object obj)
        {
            lock (ProfileViews)
            {
                ProfileViews.Clear();
            }
        }
    }
}
