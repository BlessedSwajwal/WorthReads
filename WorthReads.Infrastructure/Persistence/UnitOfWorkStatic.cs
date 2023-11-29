using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using WorthReads.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWorkStatic : IUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly IReadsRepository _readsRepository;
    private readonly IPdfContainerRepository _pdfContainerRepository;
    private bool _isDisposed = false;

    public UnitOfWorkStatic()
    {
        _userRepository = new UserRepository();
        _readsRepository = new ReadsRepositoryStatic();
        _pdfContainerRepository = new PdfContainerRepositoryStatic();
    }

    public IUserRepository UserRepository => _userRepository;

    public IReadsRepository ReadsRepository => _readsRepository;

    public IPdfContainerRepository PdfContainerRepository => _pdfContainerRepository;

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (!this._isDisposed)
            {

            }
        }
        this._isDisposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task SaveAsync()
    {
        return Task.CompletedTask;
    }
}
