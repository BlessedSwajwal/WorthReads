using Application.Common.Interfaces.Repositories;
using Domain.PdfContainer;

namespace Infrastructure.Persistence.Repositories;

public class PdfContainerRepositoryStatic : IPdfContainerRepository
{
    private static readonly List<PdfContainer> _pdfContainers = new List<PdfContainer>();
    public async Task AddAsync(PdfContainer container)
    {
        _pdfContainers.Add(container);
    }

    public async Task<PdfContainer> GetFromIdAsync(PdfContainerId pdfContainerId)
    {
        var pc = _pdfContainers.FirstOrDefault(pc => pc.Id == pdfContainerId);
        if (pc is null) return PdfContainer.ContainerEmpty;
        return pc;
    }
}
