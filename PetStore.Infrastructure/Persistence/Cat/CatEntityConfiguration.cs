using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Persistence.Cat;

public class CatEntityConfiguration : IEntityTypeConfiguration<Domain.CatAggregate.Cat>
{
    public void Configure(EntityTypeBuilder<Domain.CatAggregate.Cat> builder)
    {
        builder.ToTable(nameof(Domain.CatAggregate.Cat));
        builder.HasKey(x => x.Id);
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
        
        builder.Property(b => b.Name);
        
        builder.Ignore(x => x.DomainEvents);
    }
}