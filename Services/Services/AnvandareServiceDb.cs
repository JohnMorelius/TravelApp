using DbRepos;
using TravelApp.Models.DTO;

namespace Services;

public class AnvandareServiceDb : IAnvandareService
{
    private readonly AnvandareDbRepos _repo;

    public AnvandareServiceDb(AnvandareDbRepos repo)
    {
        _repo = repo;
    }

    public async Task<ResponsePageDto<AnvandareMedKommentarerDto>> ReadAnvandareMedKommentarerAsync(int pageNumber, int pageSize)
    {
        return await _repo.ReadAnvandareMedKommentarerAsync(pageNumber, pageSize);
    }
}