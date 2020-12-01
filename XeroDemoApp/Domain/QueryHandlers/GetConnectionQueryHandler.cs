using System.Threading;
using System.Threading.Tasks;
using MediatR;
using XeroDemoApp.Domain.Entities.Enums;
using XeroDemoApp.Domain.Queries;
using XeroDemoApp.Domain.Queries.Results;
using XeroDemoApp.Infrastructure.Data.Abstractions;

namespace XeroDemoApp.Domain.QueryHandlers
{
    public class GetConnectionQueryHandler : IRequestHandler<GetConnectionQuery, GetConnectionQueryResult>
    {
        private readonly ITokenRepository _tokenRepository;

        public GetConnectionQueryHandler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<GetConnectionQueryResult> Handle(GetConnectionQuery request, CancellationToken cancellationToken)
        {
            var token = await _tokenRepository.Get();
            return token is null
                ? new GetConnectionQueryResult(ConnectionState.Disconnected)
                : new GetConnectionQueryResult(ConnectionState.Connected);
        }
    }
}
