using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentProfileSingleConversion : IStudentSingleConversion
    {
        internal static StudentProfileView GetProfile(GetInfoAboutStudentProfile_Result result)
        {
            if (result.GroupName == "null")
                return new StudentProfileView() { Name = result.Name, Surname = result.Surname, GroupName = "Not in the group", Login = result.Login, Password = result.Password };
            else
                return new StudentProfileView() { Name = result.Name, Surname = result.Surname, GroupName = result.GroupName, Login = result.Login, Password = result.Password };
        }
    }
}
