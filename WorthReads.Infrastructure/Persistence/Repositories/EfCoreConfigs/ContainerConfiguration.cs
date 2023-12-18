using Domain.PdfContainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorthReads.Domain.Users.ValueObjects;

namespace Infrastructure.Persistence.Repositories.EfCoreConfigs;

public class ContainerConfiguration : IEntityTypeConfiguration<PdfContainer>
{
    public void Configure(EntityTypeBuilder<PdfContainer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, val => PdfContainerId.Create(val));

        builder.Property(x => x.OwnerId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, val => UserId.Create(val));


        builder.OwnsMany(x => x.ReadsUrl, rb =>
        {
            rb.Property<int>("Value");
            rb.WithOwner().HasForeignKey("ContainerId");
            rb.HasKey(["Value", "ContainerId"]);

        });


        builder.Navigation(x => x.ReadsUrl)
            .HasField("_readsUrl")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


    }
}
