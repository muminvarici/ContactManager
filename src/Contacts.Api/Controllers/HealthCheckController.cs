using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[Route("healthcheck")]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Get health check
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
