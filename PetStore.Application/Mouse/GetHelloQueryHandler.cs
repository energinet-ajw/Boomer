using PetStore.Application.Base;

namespace PetStore.Application.Mouse;

public class GetMouseQuery : IQuery<string>
{
}

public class GetHelloQueryHandler : IQueryHandler<GetMouseQuery, string>
{
    public async Task<string> Handle(GetMouseQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult("Hello...");
    }
}