namespace TravelApp.Models;

public enum Kategori
{
    Restaurang,
    Cafe,
    Arkitektur
}

public class Sevardhet
{
    public int Id { get; set; }
    public string Rubrik { get; set; } = string.Empty;
    public string Beskrivning { get; set; } = string.Empty;
    public Kategori Kategori { get; set; }
    public string Adress { get; set; } = string.Empty;

    // Foreign key till Ort
    public int OrtId { get; set; }
    public Ort? Ort { get; set; }

    // En sevärdhet kan ha många kommentarer
    public ICollection<Kommentar> Kommentarer { get; set; } = new List<Kommentar>();
}