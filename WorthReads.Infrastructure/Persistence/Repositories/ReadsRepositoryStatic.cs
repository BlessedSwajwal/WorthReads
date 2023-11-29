using Application.Common.Interfaces.Repositories;
using Domain.Reads;

namespace Infrastructure.Persistence.Repositories;

public class ReadsRepositoryStatic : IReadsRepository
{
    private readonly static List<Read> _reads = new List<Read>();
    public async Task<Read> GetPdfDetailsAsync(Uri url)
    {
        var read = _reads.FirstOrDefault(r => r.Id == url);
        if (read is null) return Read.EmptyReads;
        return read;
    }
}
