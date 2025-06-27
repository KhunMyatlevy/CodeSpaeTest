using Microsoft.AspNetCore.Mvc;
using MyApiApp.Data;
using MyApiApp.Model;
using Microsoft.EntityFrameworkCore;
using MyApiApp.DTO;

namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "AddTeacher")]
        public async Task<IActionResult> AddTeacher([FromBody] SaveTeacherRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Teacher name is required");
            }

            var teacher = new Teacher
            {
                Name = request.Name,
                subject = request.Subject
            };

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher added successfully" });
        }

        [HttpGet("{id}", Name = "GetTeacher")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet(Name = "GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return Ok(teachers);
        }

        [HttpPut("updateByName/{name}", Name = "UpdateTeacherByName")]
        public async Task<IActionResult> UpdateTeacherByName(string name, [FromBody] TeacherUpadateRequest request)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required to update teacher" });
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Name == name);

            if (teacher == null)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            teacher.Name = request.Name;
            await _context.SaveChangesAsync();


            return Ok(new { message = "Teacher updated successfully" });
        }

        [HttpDelete("deleteByName/{name}", Name = "DeleteTeacherByName")]
        public async Task<IActionResult> DeleteTeacherByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required to delete teacher" });
            }

            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Name == name);

            if (teacher == null)
            {
                return NotFound(new { message = "Teacher not found" });
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Teacher deleted successfully" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTeacherByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new { message = "Name is required for searching" });
            }

            var teachers = await _context.Teachers
            .Where(t => t.Name.Contains(name))
            .ToListAsync();

            if (teachers == null || teachers.Count == 0)
            {
                return NotFound(new { message = "No teachers found with the given name" });
            }

            return Ok(teachers);
        }

        [HttpGet("searchBySubject")]
        public async Task<IActionResult> SearchTeacherBySubject([FromQuery] string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                return BadRequest(new { message = "Subject is required for searching" });
            }

            var teachers = await _context.Teachers
            .Where(t => t.subject.Contains(subject))
            .ToListAsync();

            if (teachers == null || teachers.Count == 0)
            {
                return NotFound(new { message = "No teachers found with given subject" });
            }

            return Ok(teachers);
        }
    }
}
