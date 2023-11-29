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
}
