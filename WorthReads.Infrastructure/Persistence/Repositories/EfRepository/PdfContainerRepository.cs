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
        _context.PdfContainers.Add(container);
        return Task.CompletedTask;
    }

    public async Task<PdfContainer> GetFromIdAsync(PdfContainerId pdfContainerId)
    {
        var container = _context.PdfContainers.FirstOrDefault(c => c.Id == pdfContainerId);
        if (container is null) return PdfContainer.ContainerEmpty;
        return container;
    }

    public async Task<List<PdfContainer>> GetPdfListAsync(IReadOnlyList<PdfContainerId> pdfContainerIds)
    {
        var res = _context.PdfContainers
                    .Where(e => pdfContainerIds.Contains(e.Id))
                    .ToList();
        return res;
    }
}
