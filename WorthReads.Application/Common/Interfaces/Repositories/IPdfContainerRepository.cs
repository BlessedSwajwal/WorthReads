using Domain.PdfContainer;

namespace Application.Common.Interfaces.Repositories;

public interface IPdfContainerRepository
{
    Task AddAsync(PdfContainer container);
}
