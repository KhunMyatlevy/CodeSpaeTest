using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApiApp.Model;
using Microsoft.EntityFrameworkCore;

namespace MyApiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Parent> Parent { get; set; } 
        public DbSet<Course> Courses { get; set; }
    }
}