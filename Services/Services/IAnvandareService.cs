using TravelApp.Models.DTO;

namespace Services;

public interface IAnvandareService
{
    Task<ResponsePageDto<AnvandareMedKommentarerDto>> ReadAnvandareMedKommentarerAsync(int pageNumber, int pageSize);
}