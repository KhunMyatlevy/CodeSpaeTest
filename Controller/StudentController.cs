using Microsoft.AspNetCore.Mvc;
using MyApiApp.Data;
using MyApiApp.Model;
using Microsoft.EntityFrameworkCore;
using MyApiApp.DTO;
namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HelloController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("Student name is required.");
            }

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

        [HttpPut(Name = "UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentUpadateRequest request, int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if(student == null)
            {
                return NotFound();
            }
            student.Name = request.Name;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Student updated successfully." });
        }

        [HttpDelete("{id}", Name = "DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student deleted successfully." });
        }
    }
}
