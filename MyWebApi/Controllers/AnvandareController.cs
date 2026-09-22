using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnvandareController : ControllerBase
{
    private readonly IAnvandareService _anvandareService;

    public AnvandareController(IAnvandareService anvandareService)
    {
        _anvandareService = anvandareService;
    }

    // D: /api/Anvandare?pageNumber=0&pageSize=10
    [HttpGet]
    public async Task<IActionResult> ReadAnvandareMedKommentarer(int pageNumber = 0, int pageSize = 10)
    {
        var result = await _anvandareService.ReadAnvandareMedKommentarerAsync(pageNumber, pageSize);
        return Ok(result);
    }
}