
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories.EfRepository;
using WorthReads.Application.Common.Interfaces.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly WorthReadsDbContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IReadsRepository _readsRepository;
    private readonly IPdfContainerRepository _pdfContainerRepository;

    public UnitOfWork(WorthReadsDbContext context)
    {
        _context = context;
        _userRepository = new UserRepository(_context);
        _readsRepository = new ReadsRepository(_context);
        _pdfContainerRepository = new PdfContainerRepository(_context);
    }

    public IUserRepository UserRepository => _userRepository;
    public IReadsRepository ReadsRepository => _readsRepository;
    public IPdfContainerRepository PdfContainerRepository => _pdfContainerRepository;
    private bool _disposed = false;

    private void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this._disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
