 using DbRepos;
using TravelApp.Models;
using TravelApp.Models.DTO;

namespace Services;

public class SevardhetServiceDb : ISevardhetService
{
    private readonly SevardhetDbRepos _repo;

    public SevardhetServiceDb(SevardhetDbRepos repo)
    {
        _repo = repo;
    }

    public async Task<ResponsePageDto<Sevardhet>> ReadSevardheterAsync(string kategori, string rubrik, string beskrivning, string land, string ort, int pageNumber, int pageSize)
    {
        return await _repo.ReadSevardheterAsync(kategori, rubrik, beskrivning, land, ort, pageNumber, pageSize);
    }

    public async Task<ResponsePageDto<Sevardhet>> ReadSevardheterUtanKommentarAsync(int pageNumber, int pageSize)
    {
        return await _repo.ReadSevardheterUtanKommentarAsync(pageNumber, pageSize);
    }

    public async Task<SevardhetDetailDto> ReadSevardhetMedKommentarerAsync(int sevardhetId, int pageNumber, int pageSize)
    {
        return await _repo.ReadSevardhetMedKommentarerAsync(sevardhetId, pageNumber, pageSize);
    }
}