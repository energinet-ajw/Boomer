using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.Queries;

public class GetMiceQuery : IQuery<IList<MouseDto>>
{
}

public class GetMiceQueryHandler : IQueryHandler<GetMiceQuery, IList<MouseDto>>
{
    private readonly IMouseRepository _mouseRepository;

    public GetMiceQueryHandler(IMouseRepository mouseRepository)
    {
        _mouseRepository = mouseRepository;
    }
    public async Task<IList<MouseDto>> Handle(GetMiceQuery query, CancellationToken token)
    {
        var mice = await _mouseRepository.GetAllAsync(token).ConfigureAwait(false);
        return mice.Select(x => new MouseDto { Id = x.Id, Name = x.Name }).ToList();
    }
}