using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiApp.Data;
using MyApiApp.Model;
using MyApiApp.DTO;

namespace MyApiApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] SaveCourseRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Course title is required");
            }

            var course = new Course
            {
                Title = request.Title,
                Credits = request.Credits,
                Department = request.Department
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Course added successfully",
                courseId = course.Id  
            });
        }

        [HttpGet("{id}", Name = "GetCourseById")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpGet(Name = "GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();
            return Ok(courses);
        }

        [HttpPut("{id}", Name = "UpdateCourseById")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseUpdateRequest request)
        {
            if (id != request.Id)
                return BadRequest(new { message = "ID mismatch" });

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            course.Title = request.Title;
            course.Credits = request.Credits;
            course.Department = request.Department;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Course updated successfully" });
        }

        [HttpDelete("{id}", Name = "DeleteCourseById")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course deleted successfully" });
        }
    }
}