namespace TravelApp.Models.DTO;

public class AnvandareMedKommentarerDto
{
    public int Id { get; set; }
    public string Namn { get; set; }
    public string Epost { get; set; }
    public List<Kommentar> Kommentarer { get; set; }
}
