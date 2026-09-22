 using Seido.Utilities.SeedGenerator;

namespace TravelApp.Models;

public class Anvandare : ISeed<Anvandare>
{
    public int Id { get; set; }
    public string Namn { get; set; }
    public string Epost { get; set; }
    public bool Seeded { get; set; }

    public ICollection<Kommentar> Kommentarer { get; set; } = new List<Kommentar>();

    public Anvandare Seed(SeedGenerator seedGenerator)
    {
        Namn = seedGenerator.FullName;
        // Epost sätts int här den byggs i repository-loopen där vi har
        // tillgång till loop-indexet, så vi kan garantera att den blir unik
        return this;
    }
}