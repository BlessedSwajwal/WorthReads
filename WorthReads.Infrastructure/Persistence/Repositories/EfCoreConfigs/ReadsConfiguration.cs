using Domain.Reads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Repositories.EfCoreConfigs;

public class ReadsConfiguration : IEntityTypeConfiguration<Read>
{
    public void Configure(EntityTypeBuilder<Read> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(id => id.ToString(), uSt => new Uri(uSt));
    }
}
