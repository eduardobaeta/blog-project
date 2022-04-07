using Microsoft.AspNetCore.Mvc;

namespace Blog.src.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}