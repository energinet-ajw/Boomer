using PetStore.Application.Base;
using PetStore.Domain.CatAggregate;

namespace PetStore.Application.Cat.Queries;

public class GetCatsQuery : IQuery<IList<CatDto>>
{
}

public class GetCatsQueryHandler : IQueryHandler<GetCatsQuery, IList<CatDto>>
{
    private readonly ICatRepository _catRepository;

    public GetCatsQueryHandler(ICatRepository catRepository)
    {
        _catRepository = catRepository;
    }
    public async Task<IList<CatDto>> Handle(GetCatsQuery query, CancellationToken token)
    {
        var cats = await _catRepository.GetAllAsync(token).ConfigureAwait(false);
        return cats.Select(x => new CatDto { Id = x.Id, Name = x.Name }).ToList();
    }
}