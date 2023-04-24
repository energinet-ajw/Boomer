using CarStore.Infrastructure.Persistence;

namespace CarStore.APi;

public record CreateCar(Guid Id, string Model);

public class CreateIssueHandler
{
    private readonly CarRepository _repository;

    public CreateIssueHandler(CarRepository repository)
    {
        _repository = repository;
    }

    // The IssueCreated event message being returned will be
    // published as a new "cascaded" message by Wolverine after
    // the original message and any related middleware has
    // succeeded
    public Guid Handle(CreateCar command)
    {
        return Guid.NewGuid();
    }
}