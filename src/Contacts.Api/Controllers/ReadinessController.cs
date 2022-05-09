using Microsoft.AspNetCore.Mvc;

namespace Contacts.Api.Controllers;

[Route("readiness")]
public class ReadinessController : ControllerBase
{
    /// <summary>
    /// Ctor
    /// </summary>
    public ReadinessController()
    {
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <remarks>Method description</remarks>
    /// <returns>Model</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            ServerTime = DateTime.Now
        });
    }
}
