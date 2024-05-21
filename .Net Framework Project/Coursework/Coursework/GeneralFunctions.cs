using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class GeneralFunctions : IGeneralFunctions
    {
        internal static string GetSubjectNameById(short subjectId)
        {
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return db.Subjects.ToList().First(x => x.Id == subjectId).Name;
            }
        }
    }
}
