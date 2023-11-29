using Domain.Reads;

namespace Infrastructure.Services;

public interface IPocketAPIService
{
    Task<List<Read>> GetPocketList();
}