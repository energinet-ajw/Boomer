using PetStore.Application.Base;

namespace PetStore.Application.Mouse;

public class GetHelloQuery : IQuery<string>
{
}

public class GetHelloQueryHandler : IQueryHandler<GetHelloQuery, string>
{
    public async Task<string> Handle(GetHelloQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult("Hello...");
    }
}