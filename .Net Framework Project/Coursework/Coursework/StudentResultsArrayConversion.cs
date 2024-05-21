using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentResultsArrayConversion : IStudentArrayConversion
    {
        private List<StudentResultView> ResultViews { get; set; }
        public StudentResultsArrayConversion()
        {
            ResultViews = new List<StudentResultView>();
        }
        internal List<StudentResultView> GetResultsForResultsView(System.Data.Entity.Core.Objects.ObjectResult<GetResultsByStudentId_Result> res)
        {
            foreach (var result in res)
            {
                ResultViews.Add(StudentResultSingleConversion.GetResult(result));
            }
            List<StudentResultView> resultViews = ResultViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearResultViews);
            return resultViews;
        }
        private void ClearResultViews(object obj)
        {
            lock (ResultViews)
            {
                ResultViews.Clear();
            }
        }
    }
}
