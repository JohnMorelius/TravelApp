namespace TravelApp.Models.DTO;

public class SevardhetDetailDto
{
    public string Kategori { get; set; }
    public string Rubrik { get; set; }
    public string Beskrivning { get; set; }
    public ResponsePageDto<Kommentar> Kommentarer { get; set; }
}