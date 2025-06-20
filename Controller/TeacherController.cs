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
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            if (teacher == null || string.IsNullOrWhiteSpace(teacher.Name))
            {
               return BadRequest("Teacher name is required");
            }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherUpadateRequest request, int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
             }

             teacher.Name = request.Name;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Teacher updated successfully" });
        }

        [HttpDelete("{id}", Name = "DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
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
                return BadRequest(new { message = "Name is required for searching"});
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
    }
}
