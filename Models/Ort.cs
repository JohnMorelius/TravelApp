namespace TravelApp.Models;

public class Ort
{
    public int Id { get; set; }
    public string Namn { get; set; } = string.Empty;

    // Foreign key till Land
    public int LandId { get; set; }
    public Land? Land { get; set; }

    // En ort kan ha många sevärdheter
    public ICollection<Sevardhet> Sevardheter { get; set; } = new List<Sevardhet>();
}