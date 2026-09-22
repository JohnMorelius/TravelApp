using DbRepos;

namespace Services;

public class SeedServiceDb : ISeedService
{
    private readonly SeedDbRepos _repo;

    public SeedServiceDb(SeedDbRepos repo)
    {
        _repo = repo;
    }

    public async Task SeedAsync()
    {
        await _repo.SeedAsync();
    }
public async Task<(int nrLanderAffected, int nrAnvandareAffected)> RemoveTestDataAsync()
{
    return await _repo.RemoveTestDataAsync(seeded: true);
}

}