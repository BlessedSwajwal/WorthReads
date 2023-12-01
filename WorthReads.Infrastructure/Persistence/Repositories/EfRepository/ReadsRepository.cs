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

    public Task<Read> GetPdfDetailsAsync(Uri url)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Read>> GetPdfsFromUrlListAsync(IReadOnlyList<Uri> urlList)
    {
        var res = await _context.Reads
                        .Where(r => urlList.Contains(r.Url))
                        .ToListAsync();
        return res;
    }
}
