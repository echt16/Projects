using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class QuestionView : ITestView
    {
        public long Id { get; internal set; }
        public string Text { get; internal set; }
        public List<PossibleAnsverView> PossibleAnsvers { get; internal set; }
        internal QuestionView()
        {
            PossibleAnsvers = new List<PossibleAnsverView>();
        }
    }
}
