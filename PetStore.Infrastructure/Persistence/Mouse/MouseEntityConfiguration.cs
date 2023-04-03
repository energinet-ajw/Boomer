using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Persistence.Mouse;

public class MouseEntityConfiguration : IEntityTypeConfiguration<Domain.MouseAggregate.Mouse>
{
    public void Configure(EntityTypeBuilder<Domain.MouseAggregate.Mouse> builder)
    {
        builder.ToTable(nameof(Mouse));
        builder.HasKey(x => x.Id);
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
    }
}