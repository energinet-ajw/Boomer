using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.Queries;

public class GetMouseQuery : IQuery<MouseDto>
{
    public GetMouseQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}

public class GetMouseQueryHandler : IQueryHandler<GetMouseQuery, MouseDto>
{
    private readonly IMouseRepository _mouseRepository;

    public GetMouseQueryHandler(IMouseRepository mouseRepository)
    {
        _mouseRepository = mouseRepository;
    }
    public async Task<MouseDto> Handle(GetMouseQuery query, CancellationToken token)
    {
        var mouse = await _mouseRepository.GetAsync(query.Id, token).ConfigureAwait(false);
        return new MouseDto { Id = mouse.Id };
    }
}