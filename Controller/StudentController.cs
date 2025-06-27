using Microsoft.AspNetCore.Mvc;
using MyApiApp.Data;
using MyApiApp.Model;
using Microsoft.EntityFrameworkCore;
using MyApiApp.DTO;

namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController (AppDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] SaveStudnetRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Student name is required.");
            }

            var student = new Student
            {
                Name = request.Name,
                Grade = request.Grade
            };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student added successfully." });
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpGet(Name = "GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpPut("updateByName/{name}", Name = "UpdateStudentByName")]
        public async Task<IActionResult> UpdateStudentByName(string name, [FromBody] StudentUpadateRequest request)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required to update student" });
            }

            var student = await _context.Students.FirstOrDefaultAsync(s => s.Name == name);

            if (student == null)
            {
                return NotFound(new { message = "Student not found" });
            }
            student.Name = request.Name;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Student updated successfully." });
        }

        [HttpDelete("deleteByName/{name}", Name = "DeleteStudentByName")]
        public async Task<IActionResult> DeleteStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required to delete student." });
            }

            var student = await _context.Students.FirstOrDefaultAsync(s => s.Name == name);

            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student deleted successfully." });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStudentByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required for searching" });
            }

            var students = await _context.Students
            .Where(s => s.Name.Contains(name))
            .ToListAsync();

            if (students == null || students.Count == 0)
            {
                return NotFound(new { message = "No students found with the given name" });
            }

            return Ok(students);
        }
    }
}
