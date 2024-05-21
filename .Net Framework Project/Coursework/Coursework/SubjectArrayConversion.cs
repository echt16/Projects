using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    internal class SubjectArrayConversion : IArrayConversion
    {
        private List<SubjectView> SubjectViews { get; set; }
        public SubjectArrayConversion() {  SubjectViews = new List<SubjectView>(); }
        internal List<SubjectView> GetSubjects(System.Data.Entity.Core.Objects.ObjectResult<GetAllSubjects_Result> res)
        {
            foreach (var subject in res)
            {
                SubjectViews.Add(SubjectSingleConversion.GetSubject(subject));
            }
            List<SubjectView> subjectViews = SubjectViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearSubjectViews);
            return subjectViews;
        }
        private void ClearSubjectViews(object obj)
        {
            lock (SubjectViews)
            {
                SubjectViews.Clear();
            }
        }
    }
}
