using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApiApp.DTO
{
    public class CourseUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Department  { get; set; }
    }
}