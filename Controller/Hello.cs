using Microsoft.AspNetCore.Mvc;

namespace MyApiApp.Controllers // ✅ Use Controllers, not Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult SayHello()
        {
            return Ok(new { message = "Hello from API!" });
        }
    }
}
