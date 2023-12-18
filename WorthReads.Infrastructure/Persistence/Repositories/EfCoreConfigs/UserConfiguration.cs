using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorthReads.Domain.Users;
using WorthReads.Domain.Users.ValueObjects;

namespace Infrastructure.Persistence.Repositories.EfCoreConfigs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(x => x.Value, val => UserId.Create(val));

        //FirstName, LName, Email and pass default.

        builder.OwnsMany(u => u.OwningPdfs, ob =>
        {
            ob.WithOwner().HasForeignKey("UserId");
            ob.HasKey(["UserId", "Value"]);
            ob.Property(o => o.Value)
                .ValueGeneratedNever();
        });

        builder.Navigation(x => x.OwningPdfs)
            .HasField("_pdfs")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
