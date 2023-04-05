using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;
using PetStore.Domain.MouseAggregate.Specifications;

namespace PetStore.Application.Mouse.Queries;

public class SearchMiceQuery : IQuery<IList<MouseDto>>
{
    public string Name { get; }

    public SearchMiceQuery(string name)
    {
        Name = name;
    }
}

public class SearchMiceQueryHandler : IQueryHandler<SearchMiceQuery, IList<MouseDto>>
{
    private readonly IMouseRepository _mouseRepository;
    public SearchMiceQueryHandler(IMouseRepository mouseRepository)
    {
        _mouseRepository = mouseRepository;
    }

    public async Task<IList<MouseDto>> Handle(SearchMiceQuery query, CancellationToken token)
    {
        var specification = new SearchByMouseNameSpecification(query.Name);
        var mice = await _mouseRepository.GetBySpecificationAsync(specification, token).ConfigureAwait(false);
        return mice.Select(x => new MouseDto { Id = x.Id, Name = x.Name }).ToList();
    }
}