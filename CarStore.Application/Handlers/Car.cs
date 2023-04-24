namespace CarStore.Application.Handlers;

public class Car
{
    public Car(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; }
    
    public string Name { get; }
}