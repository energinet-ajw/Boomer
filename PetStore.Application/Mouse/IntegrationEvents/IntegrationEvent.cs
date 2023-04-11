using PetStore.Application.Base;

namespace PetStore.Application.Mouse.IntegrationEvents;

public abstract class IntegrationEvent : IIntegrationEvent
{
    protected IntegrationEvent()
    {
        Data = Array.Empty<byte>();
    }
    
    public Guid Id;
    public byte[] Data;
}