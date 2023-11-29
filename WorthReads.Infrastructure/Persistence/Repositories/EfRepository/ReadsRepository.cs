using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Persistence.Repositories.EfRepository;

public class ReadsRepository : IReadsRepository
{
    private readonly WorthReadsDbContext _context;
    public ReadsRepository(WorthReadsDbContext context)
    {
        _context = context;
    }
}
