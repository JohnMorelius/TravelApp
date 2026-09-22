 using TravelApp.Models;
using TravelApp.Models.DTO;

namespace Services;

public interface ISevardhetService
{
    Task<ResponsePageDto<Sevardhet>> ReadSevardheterAsync(string kategori, string rubrik, string beskrivning, string land, string ort, int pageNumber, int pageSize);
    Task<ResponsePageDto<Sevardhet>> ReadSevardheterUtanKommentarAsync(int pageNumber, int pageSize);
    Task<SevardhetDetailDto> ReadSevardhetMedKommentarerAsync(int sevardhetId, int pageNumber, int pageSize);
}