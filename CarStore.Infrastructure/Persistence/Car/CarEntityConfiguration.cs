using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarStore.Infrastructure.Persistence.Car;

public class CatEntityConfiguration : IEntityTypeConfiguration<Application.Handlers.Car>
{
    public void Configure(EntityTypeBuilder<Application.Handlers.Car> builder)
    {
        builder.ToTable(nameof(Car));
        builder.HasKey(x => x.Id);
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
        
        builder.Property(b => b.Name);
    }
}