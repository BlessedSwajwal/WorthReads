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

    public Task<PdfContainer> GetFromIdAsync(PdfContainerId pdfContainerId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PdfContainer>> GetPdfListAsync(IReadOnlyList<PdfContainerId> pdfContainerIds)
    {
        var res = _context.PdfContainers
                    .Where(e => pdfContainerIds.Contains(e.Id))
                    .ToList();
        return res;
    }
}
