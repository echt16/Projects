using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class SubjectsViewFunctions : IViewFunctions
    {
        private SubjectArrayConversion SubjectArrayConversion { get; set; }
        public SubjectsViewFunctions() { SubjectArrayConversion = new SubjectArrayConversion(); }
        internal List<SubjectView> GetAllSubjects()
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return SubjectArrayConversion.GetSubjects(db.GetAllSubjects());
            }
        }
    }
}
