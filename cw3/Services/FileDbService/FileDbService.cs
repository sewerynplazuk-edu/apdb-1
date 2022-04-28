using Cw3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cw3.Services
{
    public class FileDbService : IFileDbService
    {
        private static readonly string FileName = "database.csv";

        private readonly IStudentCoder _studentCoder;
        private readonly IStudentValidator _studentValidator;
        public FileDbService(IStudentCoder studentCoder, IStudentValidator studentValidator)
        {
            _studentCoder = studentCoder;
            _studentValidator = studentValidator;
        }

        public IEnumerable<Student> GetStudents()
        {
            return ReadStudentsFromFile();
        }

        public Student GetStudent(string indexNumber)
        {
            return ReadStudentsFromFile().Find(s => s.IndexNumber == indexNumber);
        }

        public bool AddStudent(Student student)
        {
            if (!_studentValidator.IsStudentValid(student))
            {
                return false;
            }

            try
            {
                var students = ReadStudentsFromFile();
                if (students is null)
                {
                    students = new List<Student>();
                }
                if (IsCollectionMemeber(student, students))
                {
                    return false;
                }
                students.Add(student);
                PersistStudents(students);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool PutStudent(Student student)
        {
            if (!_studentValidator.IsStudentValid(student))
            {
                return false;
            }

            try
            {
                var students = ReadStudentsFromFile();

                if (!IsCollectionMemeber(student, students))
                {
                    return false;
                }

                foreach (Student element in students.Where(s => s.IndexNumber == student.IndexNumber))
                {
                    element.FirstName = student.FirstName;
                    element.LastName = student.LastName;
                    element.BirthDate = student.BirthDate;
                    element.Major = student.Major;
                    element.StudyMode = student.StudyMode;
                    element.Email = student.Email;
                    element.FathersName = student.FathersName;
                    element.MothersName = student.MothersName;
                }
                PersistStudents(students);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteStudent(string indexNumber)
        {
            try
            {
                var students = ReadStudentsFromFile();
                int index = students.FindIndex(e => e.IndexNumber == indexNumber);
                if (index == -1)
                {
                    return false;
                }
                students.RemoveAt(index);
                PersistStudents(students);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool IsCollectionMemeber(Student student, List<Student> students)
        {
            return students.Any(s => s.IndexNumber == student.IndexNumber);
        }

        private List<Student> ReadStudentsFromFile()
        {
            var list = new List<Student>();
            if (!File.Exists(FileDbService.FileName))
            {
                return list;
            }

            string fileContent = File.ReadAllText(FileDbService.FileName);
            string[] lines = fileContent.Split("\n");
            foreach (string line in lines)
            {
                if (String.IsNullOrEmpty(line))
                {
                    continue;
                }

                try
                {
                    Student student = _studentCoder.DecodeStudent(line);
                    list.Add(student);
                }
                catch
                {
                    return null;
                }
            }

            return list;
        }

        private void PersistStudents(List<Student> students)
        {
            File.WriteAllLines(FileDbService.FileName, students.Select(s => _studentCoder.EncodeStudent(s)));
        }
    }
}
