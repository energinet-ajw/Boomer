using MediatR;

namespace Boomer.Application.Queries
{
    public class GetHelloQuery : IRequest<string> { }

    public class GetHelloQueryHandler : IRequestHandler<GetHelloQuery, string>
    {
        public async Task<string> Handle(GetHelloQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult("Hello..."); 
        }
    }
}
