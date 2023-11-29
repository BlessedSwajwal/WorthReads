using Domain.Reads;

namespace Application.Common.Services;

public interface IPocketAPIService
{
    Task<List<Read>> GetPocketList();
}