using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class TeacherEducationalEstablishmentViewFunctions : TeacherViewFuctions
    {
        internal static List<string> GetEducationalEstablishmentForRegistrationWindow()
        {
            using(TestApplicationDBContext db = new TestApplicationDBContext())
            {
                return db.EducationalEstablishments.Select(x => x.Name + ", " + x.City.Name).ToList();
            }
        }
    }
}
