using System;
using System.Text.RegularExpressions;
using Cw3.Models;

namespace Cw3
{
	public class StudentValidator : IStudentValidator
	{
        public bool IsStudentValid(Student student)
        {
            if (
                student.FirstName is null ||
                student.LastName is null ||
                student.IndexNumber is null ||
                student.BirthDate is null ||
                student.Major is null ||
                student.StudyMode is null ||
                student.Email is null ||
                student.FathersName is null ||
                student.MothersName is null)
            {
                return false;
            }

            var regex = new Regex(@"s[0-9]*");
            var match = regex.Match(student.IndexNumber);
            return match.Success;
        }
    }
}

