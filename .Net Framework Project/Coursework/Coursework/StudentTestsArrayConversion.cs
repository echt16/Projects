using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentTestsArrayConversion : IStudentArrayConversion
    {
        private List<StudentTestView> TestViews { get; set; }
        public StudentTestsArrayConversion() 
        {
            TestViews = new List<StudentTestView>();
        }
        internal List<StudentTestView> GetTestsForTestsView(System.Data.Entity.Core.Objects.ObjectResult<GetTestsForStudent_Result> res)
        {
            foreach (var test in res)
            {
                TestViews.Add(StudentTestSingleConversion.GetTest(test));
            }
            List<StudentTestView> testViews = TestViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearTestViews);
            return testViews;
        }
        internal List<StudentTestView> GetTestsForTestsView(System.Data.Entity.Core.Objects.ObjectResult<GetTestsForGroup_Result> res)
        {
            foreach (var test in res)
            {
                TestViews.Add(StudentTestSingleConversion.GetTest(test));
            }
            List<StudentTestView> testViews = TestViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearTestViews);
            return testViews;
        }
        internal List<StudentTestView> GetTestsForTestsView(System.Data.Entity.Core.Objects.ObjectResult<GetAllAvailibleTestsBySubject_Result> res)
        {
            foreach (var test in res)
            {
                TestViews.Add(StudentTestSingleConversion.GetTest(test));
            }
            List<StudentTestView> testViews = TestViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearTestViews);
            return testViews;
        }
        internal List<StudentTestView> GetTestsForTestsView(System.Data.Entity.Core.Objects.ObjectResult<GetTestsByRequest_Result> res)
        {
            foreach(var test in res)
            {
                TestViews.Add(StudentTestSingleConversion.GetTest(test));
            }
            List<StudentTestView> testViews = TestViews.ToList();
            ThreadPool.QueueUserWorkItem(ClearTestViews);
            return testViews;
        }
        private void ClearTestViews(object obj)
        {
            lock (TestViews)
            {
                TestViews.Clear();
            }
        }
    }
}
