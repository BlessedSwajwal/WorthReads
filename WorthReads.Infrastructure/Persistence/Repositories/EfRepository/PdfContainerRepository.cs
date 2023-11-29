using Application.Common.Interfaces.Repositories;
using Domain.PdfContainer;

namespace Infrastructure.Persistence.Repositories.EfRepository;

public class PdfContainerRepository : IPdfContainerRepository
{
    private readonly WorthReadsDbContext _context;

    public PdfContainerRepository(WorthReadsDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(PdfContainer container)
    {
        throw new NotImplementedException();
    }
}
