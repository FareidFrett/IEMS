using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IEMS_API.Data;

namespace IEMS_API.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public HealthController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return Ok("Healthy");
    }

    [HttpGet("database")]
    public async Task<ActionResult<string>> CheckDatabase()
    {
        try
        {
            await _dbContext.Database.CanConnectAsync();
            return Ok("Database is reachable");
        }
        catch (Exception ex)
        {
            return StatusCode(503, $"Database is unreachable: {ex.Message}");
        }
    }
}
