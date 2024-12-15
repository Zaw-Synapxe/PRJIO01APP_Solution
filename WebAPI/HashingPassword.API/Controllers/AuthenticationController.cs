using HashingPassword.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HashingPassword.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly PasswordHasher<AppUser> _passwordHasher;
        private static List<AppUser> Users = [];

        public AuthenticationController()
        {
            _passwordHasher = new PasswordHasher<AppUser>();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Register(AppUser user)
        {
            if (user.Password != null)
            {
                var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
                user.Password = hashedPassword;
                Users.Add(user);

                await Task.Delay(500);

                return Ok(user);
            }
            else
            {
                return BadRequest("Empty Password ...");
            }

            //
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUser user) 
        {
            if (user.Password != null)
            {
                var _user = Users.FirstOrDefault(_ => _.Email == user.Email);
                if (_user != null)
                {
                    if(_user.Password != null)
                    {
                        var result = _passwordHasher.VerifyHashedPassword(user, _user.Password, user.Password);

                        await Task.Delay(500);

                        return result == PasswordVerificationResult.Success ? Ok(_user) : Unauthorized();
                    }
                    else
                    {
                        return BadRequest("Empty Password (at Databae) ...");
                    }
                }
                else
                {
                    return BadRequest("User Not Found ...");
                }
            }
            else
            {
                return BadRequest("Empty Password ...");
            }
            //
        }

        //
    }
}
