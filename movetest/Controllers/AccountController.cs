using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;

namespace movetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!IsValidCredentials(model.Email, model.Password))
            {
                return Unauthorized();
            }

            var claims = new List<Claim> {
                            new Claim(ClaimTypes.Email, model.Email, ClaimValueTypes.String)
                         };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok();
        }

        private bool IsValidCredentials(string email, string password)
        {
            return (email == _configuration["AppSettings:UserCredentials:Username"] && 
                HashPassword(password) == _configuration["AppSettings:UserCredentials:Password"]);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", string.Empty);

            return hashedPassword;
        }
    }
}
