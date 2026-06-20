using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("health-check")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        public IActionResult GetStatus() => Ok(new {status = "Healthy", timestamp = DateTime.UtcNow});
    }
}
