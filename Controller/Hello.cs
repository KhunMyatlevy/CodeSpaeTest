using Microsoft.AspNetCore.Mvc;
using  MyApiApp.Data;
using MyApiApp.Model;

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

        [HttpGet]
        public IActionResult SayHello()
        {
            return Ok(new { message = "Hello from API!" });
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Name))
            {
                return BadRequest("Student name is required.");
            }

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        
    }
}
