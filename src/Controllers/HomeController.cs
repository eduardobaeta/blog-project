using Microsoft.AspNetCore.Mvc;

namespace Blog.src.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}