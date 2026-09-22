using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InfoController : ControllerBase
{
    private readonly IInfoService _infoService;

    public InfoController(IInfoService infoService)
    {
        _infoService = infoService;
    }

    [HttpGet]
    public async Task<IActionResult> ReadDbInfo()
    {
        var result = await _infoService.ReadDbInfoAsync();
        return Ok(result);
    }
}