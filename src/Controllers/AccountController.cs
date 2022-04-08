using Blog.Data;
using Blog.Models;
using Blog.src.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Blog.src.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost("v1/accounts")]
        public async Task<IActionResult> Post([FromServices] BlogDataContext context, [FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var password = PasswordGenerator.Generate(25, true, true);
                var user = new User()
                    {
                        Name = model.Name, 
                        Email = model.Email, 
                        Slug = model.Email.Replace('@', '-').Replace('.', '-'),
                        PasswordHash = PasswordHasher.Hash(password)
                    };
                
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                
                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = user.Email, password
                }));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<string>(e.Message));
            }
        }

        [HttpPost("v1/accounts/login")]
        public IActionResult Login([FromServices] BlogDataContext context, [FromServices] TokenService tokenService, [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var user = context.Users.AsNoTracking().Include(x => x.Roles)
                    .FirstOrDefault(x => x.Email == model.Email);
                
                if (user == null)
                    return StatusCode(401, new ResultViewModel<string>("E-mail or password invalid"));

                if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                    return StatusCode(401, new ResultViewModel<string>("E-mail or password invalid"));

                var token = tokenService.GenerateToken(user);
                return Ok(new ResultTokenViewModel<dynamic>(token));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<string>("Internal server fail"));
            }
            
        }


    }
}