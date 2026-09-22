namespace TravelApp.Models;

public class Kommentar
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime Skapad { get; set; } = DateTime.UtcNow;

    // Foreign key till Sevardhet
    public int SevardhetId { get; set; }
    public Sevardhet? Sevardhet { get; set; }

    //  en till Foreign key till Anvandare
    public int AnvandareId { get; set; }
    public Anvandare? Anvandare { get; set; }
}