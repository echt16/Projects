using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class TeacherViewFuctions : ITeacherViewFunctions
    {
        internal int TeacherId { get; set; }
        public TeacherViewFuctions(int teacherId)
        {
            TeacherId = teacherId;
        }
        public TeacherViewFuctions()
        {

        }
    }
}
