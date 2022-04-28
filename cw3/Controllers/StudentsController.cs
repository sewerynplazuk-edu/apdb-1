using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IFileDbService _fileDbService;
        public StudentsController(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _fileDbService.GetStudents();
            if (students is null)
            {
                return NotFound("Nie znaleziono studentów");
            }
            return Ok(students);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber)
        {
            var student = _fileDbService.GetStudent(indexNumber);
            if (student is null) return NotFound("Nie znaleziono studenta");
            return Ok(student);
        }

        [HttpPut]
        public IActionResult PutStudent(Student student)
        {
            var result = _fileDbService.PutStudent(student);
            if (result)
            {
                return Created("", student);
            } else
            {
                return BadRequest("Failed to put student");
            }
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            var result = _fileDbService.AddStudent(student);
            if (result)
            {
                return Created("", student);
            }
            else
            {
                return BadRequest("Failed to add student");
            }
        }

        [HttpDelete("{indexNumber}")]
        public IActionResult DeleteStudent(string indexNumber)
        {
            var result = _fileDbService.DeleteStudent(indexNumber);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound("Student not found");
            }
        }
    }
}
