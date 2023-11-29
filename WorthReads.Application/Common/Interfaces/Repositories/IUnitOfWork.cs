using WorthReads.Application.Common.Interfaces.Repositories;

namespace Application.Common.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public IReadsRepository ReadsRepository { get; }
    public IPdfContainerRepository PdfContainerRepository { get; }
    Task SaveAsync();
}
