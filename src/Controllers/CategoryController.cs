using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.src.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Get([FromServices] BlogDataContext context)
        {
            try
            {
                return Ok(await context.Categories.AsNoTracking().ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}