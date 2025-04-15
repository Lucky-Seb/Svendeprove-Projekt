using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaekwondoOrchestration.ApiService.Services;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("debug")]
        public IActionResult Debug()
        {
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                .Select(c => c.Value);

            return Ok(new
            {
                User.Identity?.Name,
                IsAuthenticated = User.Identity?.IsAuthenticated,
                Roles = roles
            });
        }
    }
}
