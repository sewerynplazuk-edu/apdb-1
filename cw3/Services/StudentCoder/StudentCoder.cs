using System;
using System.Text;
using Cw3.Models;

namespace Cw3.Services
{
    public class StudentCoder : IStudentCoder
    {
        public Student DecodeStudent(string line)
        {
            string[] values = line.Split(",");
            if (values.Length != 9)
            {
                throw new ArgumentException("Invalid number of values");
            }
            return new Student
            {
                FirstName = values[0],
                LastName = values[1],
                IndexNumber = values[2],
                BirthDate = values[3],
                Major = values[4],
                StudyMode = values[5],
                Email = values[6],
                FathersName = values[7],
                MothersName = values[8]
            };
        }

        public string EncodeStudent(Student student)
        {
            return new StringBuilder()
                .Append($"{student.FirstName},")
                .Append($"{student.LastName},")
                .Append($"{student.IndexNumber},")
                .Append($"{student.BirthDate},")
                .Append($"{student.Major},")
                .Append($"{student.StudyMode},")
                .Append($"{student.Email},")
                .Append($"{student.FathersName},")
                .Append($"{student.MothersName}")
                .ToString();
        }
    }
}