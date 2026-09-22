 using Microsoft.EntityFrameworkCore;

using TravelApp.Models;
using TravelApp.Models.DTO;

namespace DbRepos;

public class SevardhetDbRepos
{
    private readonly TravelAppDbContext _dbContext;

    public SevardhetDbRepos(TravelAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // A: Filtrerar på kategori, rubrik, beskrivning, land och ort
    public async Task<ResponsePageDto<Sevardhet>> ReadSevardheterAsync(
        string kategori, string rubrik, string beskrivning, string land, string ort,
        int pageNumber, int pageSize)
    {
        rubrik ??= "";
        beskrivning ??= "";
        land ??= "";
        ort ??= "";

        Kategori? kategoriFilter = null;
        if (!string.IsNullOrWhiteSpace(kategori) && Enum.TryParse<Kategori>(kategori, true, out var parsedKategori))
        {
            kategoriFilter = parsedKategori;
        }

        var query = _dbContext.Sevardhet
            .Include(s => s.Ort)
                .ThenInclude(o => o!.Land)
            .AsNoTracking()
            .Where(s =>
                s.Rubrik.ToLower().Contains(rubrik.ToLower())
                && s.Beskrivning.ToLower().Contains(beskrivning.ToLower())
                && (s.Ort == null || s.Ort.Namn.ToLower().Contains(ort.ToLower()))
                && (s.Ort == null || s.Ort.Land == null || s.Ort.Land.Namn.ToLower().Contains(land.ToLower()))
                && (kategoriFilter == null || s.Kategori == kategoriFilter));

        var ret = new ResponsePageDto<Sevardhet>()
        {
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    // B: Sevärdheter som saknar kommentarer
    public async Task<ResponsePageDto<Sevardhet>> ReadSevardheterUtanKommentarAsync(int pageNumber, int pageSize)
    {
        var query = _dbContext.Sevardhet
            .AsNoTracking()
            .Where(s => !s.Kommentarer.Any());

        var ret = new ResponsePageDto<Sevardhet>()
        {
            DbItemsCount = await query.CountAsync(),
            PageItems = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            PageNr = pageNumber,
            PageSize = pageSize
        };
        return ret;
    }

    // C: En sevärdhets detaljer + alla dess kommentarer (paginerade)
    public async Task<SevardhetDetailDto> ReadSevardhetMedKommentarerAsync(int sevardhetId, int pageNumber, int pageSize)
    {
        var sevardhet = await _dbContext.Sevardhet
            .AsNoTracking()
            .FirstAsync(s => s.Id == sevardhetId);

        var kommentarQuery = _dbContext.Kommentar
            .AsNoTracking()
            .Where(k => k.SevardhetId == sevardhetId);

        var ret = new SevardhetDetailDto()
        {
            Kategori = sevardhet.Kategori.ToString(),
            Rubrik = sevardhet.Rubrik,
            Beskrivning = sevardhet.Beskrivning,
            Kommentarer = new ResponsePageDto<Kommentar>()
            {
                DbItemsCount = await kommentarQuery.CountAsync(),
                PageItems = await kommentarQuery
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToListAsync(),
                PageNr = pageNumber,
                PageSize = pageSize
            }
        };
        return ret;
    }
}