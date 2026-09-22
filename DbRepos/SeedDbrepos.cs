 using Microsoft.EntityFrameworkCore;
using Seido.Utilities.SeedGenerator;
using TravelApp.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DbRepos;

public class SeedDbRepos
{
    private readonly TravelAppDbContext _dbContext;

    public SeedDbRepos(TravelAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        // "Robust seeding" - kör vi den flera gånger ska den inte skapa dubbletter
        if (await _dbContext.Land.AnyAsync(l => l.Seeded))
            return;

        var seedGenerator = new SeedGenerator();

        // 1. Länder - vi skapar de 4 kända länderna direkt, inte slumpmässigt,
        //    så vi garanterar exakt de länder som finns i seed-datan (inga dubbletter).
        var landNamn = new[] { "Sweden", "Norway", "Denmark", "Finland" };
        var lander = landNamn.Select(namn => new Land { Namn = namn, Seeded = true }).ToList();
        _dbContext.Land.AddRange(lander);
        await _dbContext.SaveChangesAsync(); // sparar nu så att Land.Id sätts av databasen

        // 2. Orter - minst 100, slumpmässigt fördelade över de 4 länderna
        var orter = new List<Ort>();
        for (int i = 0; i < 120; i++)
        {
            var land = seedGenerator.FromList(lander);
            orter.Add(new Ort
            {
                Namn = seedGenerator.City(land.Namn),
                LandId = land.Id
            });
        }
        _dbContext.Ort.AddRange(orter);
        await _dbContext.SaveChangesAsync(); // sparar så att Ort.Id sätts

        // 3. Sevärdheter - minst 1000, slumpmässigt fördelade över orterna
        var sevardheter = new List<Sevardhet>();
        for (int i = 0; i < 1100; i++)
        {
            var ort = seedGenerator.FromList(orter);
            var land = lander.First(l => l.Id == ort.LandId);

            sevardheter.Add(new Sevardhet
            {
                Rubrik = string.Join(" ", seedGenerator.LatinWords(2)),
                Beskrivning = seedGenerator.LatinParagraph,
                Kategori = seedGenerator.FromEnum<Kategori>(),
                Adress = seedGenerator.StreetAddress(land.Namn),
                OrtId = ort.Id
            });
        }
        _dbContext.Sevardhet.AddRange(sevardheter);
        await _dbContext.SaveChangesAsync(); // sparar så att Sevardhet.Id sätts

        // 4. Användare - minst 50, med garanterat unik e-post (index i e-posten)
        var anvandare = new List<Anvandare>();
        for (int i = 1; i <= 60; i++)
        {
            var user = new Anvandare { Seeded = true }.Seed(seedGenerator);

            var namnDelar = user.Namn.Split(' ');
            var baseEmail = seedGenerator.Email(namnDelar[0], namnDelar[1]);
            var atIdx = baseEmail.IndexOf('@');
            user.Epost = $"{baseEmail[..atIdx]}{i}{baseEmail[atIdx..]}";

            anvandare.Add(user);
        }
        _dbContext.Anvandare.AddRange(anvandare);
        await _dbContext.SaveChangesAsync(); // sparar så att Anvandare.Id sätts

        // 5. Kommentarer - 0 till 20 st per sevärdhet, kopplade till slumpad användare
        var kommentarer = new List<Kommentar>();
        foreach (var sevardhet in sevardheter)
        {
            int antal = seedGenerator.Next(0, 21); // 0-20 (Next är exkluderande i övre gränsen)
            for (int i = 0; i < antal; i++)
            {
                var user = seedGenerator.FromList(anvandare);
                kommentarer.Add(new Kommentar
                {
                    Text = seedGenerator.LatinSentence,
                    SevardhetId = sevardhet.Id,
                    AnvandareId = user.Id
                });
            }
        }
        _dbContext.Kommentar.AddRange(kommentarer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<(int nrLanderAffected, int nrAnvandareAffected)> RemoveTestDataAsync(bool seeded = true)
    {
        var connection = _dbContext.Database.GetDbConnection();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = "dbo.spDeleteTestData";

        var seededParam = new SqlParameter("@seeded", seeded);
        var nrLanderParam = new SqlParameter("@nrLanderAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };
        var nrAnvandareParam = new SqlParameter("@nrAnvandareAffected", SqlDbType.Int) { Direction = ParameterDirection.Output };

        command.Parameters.Add(seededParam);
        command.Parameters.Add(nrLanderParam);
        command.Parameters.Add(nrAnvandareParam);

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();

        return ((int)nrLanderParam.Value, (int)nrAnvandareParam.Value);
    }
}