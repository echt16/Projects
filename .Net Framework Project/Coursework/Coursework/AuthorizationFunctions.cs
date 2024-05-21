using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class AuthorizationFunctions : EnteringFunctions
    {
        internal int Id { get; set; }
        internal AuthorizationFunctions(int type, string login, string password) : base(login, password)
        {
            Type = type;
            Id = -1;
        }

        internal AuthorizationFunctions(string login, string password) : base(login, password)
        {

        }
        internal AuthorizationFunctions()
        {

        }

        internal bool CheckAuthorization(out int id)
        {
            bool b = false;
            if (Type != -1)
            {
                ObjectParameter objpid = new ObjectParameter("Id", typeof(int));
                ObjectParameter objpresult = new ObjectParameter("Result", typeof(bool));
                using (TestApplicationDBContext db = new TestApplicationDBContext())
                {
                    db.CheckAuthorization(Login, Password, (byte)Type, objpresult, objpid);
                }
                b = (bool)objpresult.Value;
                if(b)
                    Id = (int)objpid.Value;
            }
            id = Id;
            return b;
        }
    }
}