 using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SevardhetController : ControllerBase
{
    private readonly ISevardhetService _sevardhetService;

    public SevardhetController(ISevardhetService sevardhetService)
    {
        _sevardhetService = sevardhetService;
    }

    // A: /api/Sevardhet?kategori=&rubrik=&beskrivning=&land=&ort=&pageNumber=0&pageSize=10
    [HttpGet]
    public async Task<IActionResult> ReadSevardheter(
        string kategori, string rubrik, string beskrivning, string land, string ort,
        int pageNumber = 0, int pageSize = 10)
    {
        var result = await _sevardhetService.ReadSevardheterAsync(kategori, rubrik, beskrivning, land, ort, pageNumber, pageSize);
        return Ok(result);
    }

    // B: /api/Sevardhet/UtanKommentar?pageNumber=0&pageSize=10
    [HttpGet("UtanKommentar")]
    public async Task<IActionResult> ReadSevardheterUtanKommentar(int pageNumber = 0, int pageSize = 10)
    {
        var result = await _sevardhetService.ReadSevardheterUtanKommentarAsync(pageNumber, pageSize);
        return Ok(result);
    }

    // C: /api/Sevardhet/5/Kommentarer?pageNumber=0&pageSize=10
    [HttpGet("{id}/Kommentarer")]
    public async Task<IActionResult> ReadSevardhetMedKommentarer(int id, int pageNumber = 0, int pageSize = 10)
    {
        var result = await _sevardhetService.ReadSevardhetMedKommentarerAsync(id, pageNumber, pageSize);
        return Ok(result);
    }
}