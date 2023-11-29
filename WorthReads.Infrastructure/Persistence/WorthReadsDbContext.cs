using Domain.PdfContainer;
using Domain.Reads;
using Microsoft.EntityFrameworkCore;
using WorthReads.Domain.Users;

namespace Infrastructure.Persistence;

public class WorthReadsDbContext : DbContext
{
    public WorthReadsDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Read> Reads { get; set; }
    public DbSet<PdfContainer> PdfContainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(WorthReadsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
