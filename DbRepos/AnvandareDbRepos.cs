using Microsoft.EntityFrameworkCore;

using TravelApp.Models;
using TravelApp.Models.DTO;

namespace DbRepos;

public class AnvandareDbRepos
{
    private readonly TravelAppDbContext _dbContext;

    public AnvandareDbRepos(TravelAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponsePageDto<AnvandareMedKommentarerDto>> ReadAnvandareMedKommentarerAsync(int pageNumber, int pageSize)
    {
        var query = _dbContext.Anvandare.AsNoTracking();

        var anvandare = await query
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pageItems = new List<AnvandareMedKommentarerDto>();
        foreach (var a in anvandare)
        {
            var kommentarer = await _dbContext.Kommentar
                .AsNoTracking()
                .Where(k => k.AnvandareId == a.Id)
                .ToListAsync();

            pageItems.Add(new AnvandareMedKommentarerDto
            {
                Id = a.Id,
                Namn = a.Namn,
                Epost = a.Epost,
                Kommentarer = kommentarer
            });
        }

        var ret = new ResponsePageDto<AnvandareMedKommentarerDto>()
        {
            DbItemsCount = await query.CountAsync(),
            PageItems = pageItems,
            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }
}