namespace TravelApp.Models.DTO;

public class ResponsePageDto<T>
{
#if DEBUG
    //Endast i debug-läge, för att kunna se anslutningssträngen vid felsökning
    public string ConnectionString { get; init; }
#endif

    public List<T> PageItems { get; init; }
    public int DbItemsCount { get; init; }

    public int PageNr { get; init; }
    public int PageSize { get; init; }
    public int PageCount => (int)Math.Ceiling((double)DbItemsCount / PageSize);
}