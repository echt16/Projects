using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class SubjectSingleConversion : ISingleConversion
    {
        internal static SubjectView GetSubject(GetAllSubjects_Result result)
        {
            return new SubjectView() { SubjectId = result.SubjectId, SubjectName = result.SubjectName };
        }
    }
}
