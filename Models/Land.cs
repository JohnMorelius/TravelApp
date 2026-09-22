 namespace TravelApp.Models;

public class Land
{
    public int Id { get; set; }
    public string Namn { get; set; }
    public bool Seeded { get; set; }

    public ICollection<Ort> Orter { get; set; } = new List<Ort>();
}