using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class TestView : ITestView
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
        public List<QuestionView> Questions { get; internal set; }
        internal TestView()
        {
            Questions = new List<QuestionView>();
        }
    }
}
