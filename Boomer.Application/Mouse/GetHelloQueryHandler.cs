using Boomer.Application.Base;

namespace Boomer.Application.Boomer
{
    public class GetHelloQuery : IQuery<string> { }

    public class GetHelloQueryHandler : IQueryHandler<GetHelloQuery, string>
    {
        public async Task<string> Handle(GetHelloQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult("Hello..."); 
        }
    }
}
