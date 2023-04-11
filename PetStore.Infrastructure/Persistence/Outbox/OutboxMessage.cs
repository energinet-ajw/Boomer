namespace PetStore.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    public OutboxMessage(byte[] data, string messageType, DateTime creationDate)
    {
        Id = Guid.NewGuid();
        Data = data;
        MessageType = messageType;
        CreationDate = creationDate;
    }

    /// <summary>
    ///     Required by Entity Framework
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    private OutboxMessage(
        Guid id,
        byte[] data,
        string messageType,
        DateTime creationDate,
        DateTime? processedDate)
    {
        Id = id;
        Data = data;
        MessageType = messageType;
        CreationDate = creationDate;
        ProcessedDate = processedDate;
    }

    public Guid Id { get; }

    public string MessageType { get; }

    public byte[] Data { get; }

    public DateTime CreationDate { get; }

    public DateTime? ProcessedDate { get; set; }

    public void SetProcessed(DateTime when)
    {
        ProcessedDate = when;
    }
}