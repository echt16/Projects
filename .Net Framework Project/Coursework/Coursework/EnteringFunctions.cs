using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class EnteringFunctions : GeneralFunctions
    {
        internal string Login { get; set; }
        internal string Password { get; set; }
        internal int Type { get; set; }
        protected EnteringFunctions(string login, string password)
        {
            Login = login;
            Password = password;
        }
        protected EnteringFunctions()
        {

        }
    }
}
