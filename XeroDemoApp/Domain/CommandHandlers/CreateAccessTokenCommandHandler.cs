using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using Xero.NetStandard.OAuth2.Token;
using XeroDemoApp.Domain.Commands;
using XeroDemoApp.Domain.Commands.Results;
using XeroDemoApp.Infrastructure.Data.Abstractions;

namespace XeroDemoApp.Domain.CommandHandlers
{
    public class CreateAccessTokenCommandHandler : IRequestHandler<CreateAccessTokenCommand, CommandResult>
        {
        private readonly IOptions<XeroConfiguration> _xeroConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenRepository _tokenRepository;

        public CreateAccessTokenCommandHandler(
            IOptions<XeroConfiguration> xeroConfig,
            IHttpClientFactory httpClientFactory,
            ITokenRepository tokenRepository)
        {
            _xeroConfig = xeroConfig;
            _httpClientFactory = httpClientFactory;
            _tokenRepository = tokenRepository;
        }

        public async Task<CommandResult> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            //If Xero Auth flow was cancelled by used
            if (string.IsNullOrWhiteSpace(request.Code)) return CommandResult.Success;

            var client = new XeroClient(_xeroConfig.Value, _httpClientFactory.CreateClient("xero"));
            var xeroToken = (XeroOAuth2Token)await client.RequestAccessTokenAsync(request.Code);

            await _tokenRepository.Create(xeroToken);
            return CommandResult.Success;
        }
    }
}
