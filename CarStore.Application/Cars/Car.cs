namespace CarStore.Application.Cars;

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