using TravelApp.Models.DTO;

namespace Services;

public interface IInfoService
{
    Task<DbInfoDto> ReadDbInfoAsync();
}