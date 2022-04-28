using Cw3.Models;

namespace Cw3
{
    public interface IStudentCoder
    {
        Student DecodeStudent(string line);
        string EncodeStudent(Student student);
    }
}

