 using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly ISeedService _seedService;

    public SeedController(ISeedService seedService)
    {
        _seedService = seedService;
    }

    [HttpPost]
    public async Task<IActionResult> Seed()
    {
        await _seedService.SeedAsync();
        return Ok("Seeding klar (eller databasen var redan seedad).");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTestData()
    {
        var (lander, anvandare) = await _seedService.RemoveTestDataAsync();
        return Ok($"Testdata borttagen: {lander} land (med allt underliggande) samt {anvandare} användare.");
    }
} 