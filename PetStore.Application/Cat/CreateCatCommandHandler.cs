using PetStore.Application.Base;
using PetStore.Domain.CatAggregate;

namespace PetStore.Application.Cat;

public class CreateCatCommand : ICommand<Guid>
{
    public CreateCatCommand(string name)
    {
        Name = name;
    }
    public string Name { get; }
}

public class CreateCatCommandHandler : ICommandHandler<CreateCatCommand, Guid>
{
    private readonly ICatRepository _catRepository;

    public CreateCatCommandHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }

    public async Task<Guid> Handle(CreateCatCommand command, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"Handling {command.GetType().Name}...").ConfigureAwait(false);
        var cat = new Domain.CatAggregate.Cat(command.Name);
        await _catRepository.AddAsync(cat).ConfigureAwait(false);
        return cat.Id;
    }
}