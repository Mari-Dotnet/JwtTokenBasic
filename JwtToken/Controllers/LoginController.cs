using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public  IActionResult Login()
        {
            string token = JwtTokenGenerate();
            return Ok(token);
        }
        private string JwtTokenGenerate()
        {
            // Three parts on JWT token
            //Header payload Signature

            //header
            var algo = SecurityAlgorithms.HmacSha256;

            //payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "marimuthu"),
                new Claim(JwtRegisteredClaimNames.Sub,"marimuthu"),
                new Claim(JwtRegisteredClaimNames.Email,"marimuthuvdotnet@gmail.com"),
                new Claim("Isadmin","True"),
                new Claim(ClaimTypes.Role, "user"),
                //new Claim("Roles","user"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            //signature
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("marimuthusecret@123456azbycxdwevfu"));
            var crededtials = new SigningCredentials(securitykey, algo);
            var token=new JwtSecurityToken("marimuthu",
                "browserclients",
                claims,
                expires:DateTime.Now.AddSeconds(120),
                signingCredentials: crededtials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
