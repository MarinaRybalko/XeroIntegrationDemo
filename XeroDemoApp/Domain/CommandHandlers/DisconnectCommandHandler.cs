using System;
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
    public class DisconnectCommandHandler : IRequestHandler<DisconnectCommand, CommandResult>
    {
        private readonly IOptions<XeroConfiguration> _xeroConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenRepository _tokenRepository;

        public DisconnectCommandHandler(
            IOptions<XeroConfiguration> xeroConfig, 
            IHttpClientFactory httpClientFactory,
            ITokenRepository tokenRepository)
        {
            _xeroConfig = xeroConfig;
            _httpClientFactory = httpClientFactory;
            _tokenRepository = tokenRepository;
        }

        public async Task<CommandResult> Handle(DisconnectCommand request, CancellationToken cancellationToken)
        {
            var client = new XeroClient(_xeroConfig.Value, _httpClientFactory.CreateClient("xero"));

            var xeroToken = await _tokenRepository.Get();
            if (xeroToken is null) return CommandResult.Fail("No Connection Found");

            var utcTimeNow = DateTime.UtcNow;

            if (utcTimeNow > xeroToken.ExpiresAtUtc)
            {
                xeroToken = (XeroOAuth2Token)await client.RefreshAccessTokenAsync(xeroToken);
                await _tokenRepository.Create(xeroToken);
            }

            var xeroTenant = xeroToken.Tenants[0];
            await client.DeleteConnectionAsync(xeroToken, xeroTenant);
            await _tokenRepository.Delete();
            return CommandResult.Success;
        }
    }
}
