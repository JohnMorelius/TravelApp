using Microsoft.EntityFrameworkCore;

using TravelApp.Models;
using TravelApp.Models.DTO;

namespace DbRepos;

public class InfoDbRepos
{
    private readonly TravelAppDbContext _dbContext;

    public InfoDbRepos(TravelAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DbInfoDto> ReadDbInfoAsync()
    {
        return await _dbContext.DbInfoView.FirstAsync();
    }
}