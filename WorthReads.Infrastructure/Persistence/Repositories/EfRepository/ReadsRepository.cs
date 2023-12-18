using Application.Common.Interfaces.Repositories;
using Domain.Reads;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.EfRepository;

public class ReadsRepository : IReadsRepository
{
    private readonly WorthReadsDbContext _context;
    public ReadsRepository(WorthReadsDbContext context)
    {
        _context = context;
    }

    public async Task<Read> GetPdfDetailsAsync(Uri url)
    {

        var read = _context.Reads.FirstOrDefault(r => r.Id == url);
        if (read is null) return Read.EmptyReads;
        return read;
    }

    public async Task<List<Read>> GetPdfsFromUrlListAsync(IReadOnlyList<Uri> urlList)
    {
        var res = await _context.Reads
                        .Where(r => urlList.Contains(r.Url))
                        .ToListAsync();
        return res;
    }
}
