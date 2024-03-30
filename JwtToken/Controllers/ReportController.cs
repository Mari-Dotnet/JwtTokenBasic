using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ReportController(IHttpContextAccessor ContextAccessor)
        {
            _contextAccessor=ContextAccessor;
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        [Route("viewreport")]
        public string Report()
        {
            ///https://gemini.google.com/app/7047ebe46e456cb2 
            var claimsPrincipal = _contextAccessor?.HttpContext?.User;
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.Email); // Subject (user ID)
            var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name); // User name
            var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);  // User role (if included)

            return "view report";
        }
    }
}
