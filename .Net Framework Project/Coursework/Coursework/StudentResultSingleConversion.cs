using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class StudentResultSingleConversion : IStudentSingleConversion
    {
        internal static StudentResultView GetResult(GetResultsByStudentId_Result result)
        {
            if (result.IsScoreVisible)
                return new StudentResultView() { Id = result.TestId, Name = result.TestName, Score = $"{result.Score}/{result.MaxScore}", Time = $"{result.Time.Day}.{result.Time.Month}.{result.Time.Year} {result.Time.Hour}:{result.Time.Minute}", TimeSpan = $"{(int)(result.TimeSpan.TotalSeconds / 3600)}h {(int)((result.TimeSpan.TotalSeconds % 3600) / 60)}m {(int)((result.TimeSpan.TotalSeconds % 3600) % 60)}s", TeacherName = result.TeacherName };
            else
                return new StudentResultView() { Id = result.TestId, Name = result.TestName, Score = "unavailable", Time = $"{result.Time.Day}.{result.Time.Month}.{result.Time.Year} {result.Time.Hour}:{result.Time.Minute}", TimeSpan = $"{(int)(result.TimeSpan.TotalSeconds / 3600)}h {(int)((result.TimeSpan.TotalSeconds % 3600) / 60)}m {(int)((result.TimeSpan.TotalSeconds % 3600) % 60)}s", TeacherName = result.TeacherName };
        }
    }
}
