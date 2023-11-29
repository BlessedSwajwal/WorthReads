using Application.Common.Interfaces.Repositories;
using Domain.Reads;

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
}
