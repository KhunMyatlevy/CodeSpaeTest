using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiApp.Data;
using MyApiApp.Model;

namespace CodeSpaeTest.Controller
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
                return BadRequest("Teacher name is required.");
            }

            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return Ok(new { message = "teacher added successfully." });

        }

        [HttpGet("{id}", Name = "GetTeacher")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();
            return Ok(teacher);
        }

        [HttpGet(Name = "GetAllTeachers")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var Teachers = await _context.Teachers.ToListAsync();
            return Ok(Teachers);
        }
    }
}       