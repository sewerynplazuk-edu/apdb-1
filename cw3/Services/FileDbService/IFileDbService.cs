using Cw3.Models;
using System.Collections.Generic;

namespace Cw3.Services
{
    public interface IFileDbService
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(string indexNumber);
        bool AddStudent(Student student);
        bool PutStudent(Student student);
        bool DeleteStudent(string indexNumber);
    }
}
