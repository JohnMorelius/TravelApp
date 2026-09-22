using DbRepos;
using TravelApp.Models.DTO;

namespace Services;

public class InfoServiceDb : IInfoService
{
    private readonly InfoDbRepos _repo;

    public InfoServiceDb(InfoDbRepos repo)
    {
        _repo = repo;
    }

    public async Task<DbInfoDto> ReadDbInfoAsync()
    {
        return await _repo.ReadDbInfoAsync();
    }
}