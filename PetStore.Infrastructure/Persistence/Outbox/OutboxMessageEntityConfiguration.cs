using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Infrastructure.Persistence.Outbox;

public class OutboxMessageEntityConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(nameof(OutboxMessage));
        builder.HasKey(x => x.Id);
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Data);
        builder.Property(x => x.MessageType);
        builder.Property(x => x.CreationDate);
        builder.Property(x => x.ProcessedDate);
    }
}