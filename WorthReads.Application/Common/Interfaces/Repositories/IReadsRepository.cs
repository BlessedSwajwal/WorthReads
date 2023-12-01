using Domain.Reads;

namespace Application.Common.Interfaces.Repositories;

public interface IReadsRepository
{
    Task<Read> GetPdfDetailsAsync(Uri url);
    Task<List<Read>> GetPdfsFromUrlListAsync(IReadOnlyList<Uri> urlList);
}
