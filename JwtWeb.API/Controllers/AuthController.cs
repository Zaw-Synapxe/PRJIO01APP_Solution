using JwtWeb.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtWeb.API.Controllers
{
    // another example at
    // https://medium.com/@meghnav274/simple-jwt-authentication-using-asp-net-core-api-5d04b496d27b

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<User> _users = new List<User>
        {
            new User{ UserName = "Admin", Password="Password"},
            new User{UserName="defaultuser", Password = "def@123"}
        };

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            // check loginuser validation
            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            // check user exist at user List (for checking at DB)
            var LoginUser = _users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            //if (user.UserName == "defaultuser" && user.Password == "def@123")
            if (LoginUser != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Secret").Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: _config.GetSection("Jwt:ValidIssuer").Value,
                    audience: _config.GetSection("Jwt:ValidAudience").Value,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5), //DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString });
            }

            return Unauthorized();
        }

    }
}
