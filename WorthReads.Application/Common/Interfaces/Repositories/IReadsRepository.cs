using Domain.Reads;

namespace Application.Common.Interfaces.Repositories;

public interface IReadsRepository
{
    Task<Read> GetPdfDetailsAsync(Uri url);
}
