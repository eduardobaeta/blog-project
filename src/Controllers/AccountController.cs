using Blog.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.src.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post()
        {
            return Ok();
        }

        [HttpPost("v1/accounts/login")]
        public IActionResult Login([FromServices] TokenService tokenService)
        {
            var token = tokenService.GenerateToken(null);

            return Ok(token);
        }


    }
}