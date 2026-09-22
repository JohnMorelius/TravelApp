 namespace Services;

public interface ISeedService
{
    Task SeedAsync();
    Task<(int nrLanderAffected, int nrAnvandareAffected)> RemoveTestDataAsync();
}