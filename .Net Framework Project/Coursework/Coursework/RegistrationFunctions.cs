using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coursework
{
    internal class RegistrationFunctions : EnteringFunctions
    {
        internal string Name {  get; set; }
        internal string Surname {  get; set; }
        internal RegistrationFunctions(string login, string password, string name, string surname) : base(login, password)
        {
            Name = name;
            Surname = surname;
        }
        internal RegistrationFunctions(string login, string password) : base(login, password)
        {  }
        internal RegistrationFunctions()
        {  }
        internal bool CheckLoginOfCurrentUser()
        {
            bool b = false;
            System.Data.Entity.Core.Objects.ObjectParameter result = new System.Data.Entity.Core.Objects.ObjectParameter("Result", typeof(bool));
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                db.ExistsLogin(Login, result);
                b = (bool)result.Value;
            }
            return b;
        }
        internal static bool CheckLogin(string login)
        {
            bool b = false;
            System.Data.Entity.Core.Objects.ObjectParameter result = new System.Data.Entity.Core.Objects.ObjectParameter("Result", typeof(bool));
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                db.ExistsLogin(login, result);
                b = (bool)result.Value;
            }
            return b;
        }
        internal int InsertIntoTableStudents()
        {
            System.Data.Entity.Core.Objects.ObjectParameter objp = new System.Data.Entity.Core.Objects.ObjectParameter("Id", 0);
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                db.InsertIntoStudents(Login, Password, Name, Surname, objp);
            }
            return (int)objp.Value;
        }
        internal int InsertIntoTableTeachers(List<byte[]> confirmation, string educationalEstablishment)
        {
            System.Data.Entity.Core.Objects.ObjectParameter objp = new System.Data.Entity.Core.Objects.ObjectParameter("Id", 0);
            using (TestApplicationDBContext db = new TestApplicationDBContext())
            {
                db.InsertIntoTeachers(Login, Password, Name, Surname, confirmation[0], confirmation[1], confirmation[2], educationalEstablishment,  objp);
            }
            return (int)objp.Value;
        }
        internal bool CheckConfirmation(List<byte[]> confirmation)
        {
            for (int i = 0; i < 3; i++)
            {
                if (confirmation[i] == null)
                    return false;
            }
            return true;
        }
    }
}
