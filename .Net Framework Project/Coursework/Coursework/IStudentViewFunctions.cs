﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal interface IStudentViewFunctions : IViewFunctions
    {
        int StudentId { get; set; }
    }
}