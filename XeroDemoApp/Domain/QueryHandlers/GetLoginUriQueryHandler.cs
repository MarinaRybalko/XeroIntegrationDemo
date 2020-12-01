using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Xero.NetStandard.OAuth2.Client;
using Xero.NetStandard.OAuth2.Config;
using XeroDemoApp.Domain.Queries;
using XeroDemoApp.Domain.Queries.Results;

namespace XeroDemoApp.Domain.QueryHandlers
{
    public class GetLoginUriQueryHandler : IRequestHandler<GetLoginUriQuery, GetLoginUriQueryResult>
    {
        private readonly IOptions<XeroConfiguration> _xeroConfig;
        private readonly IHttpClientFactory _httpClientFactory;

        public GetLoginUriQueryHandler(IOptions<XeroConfiguration> xeroConfig, IHttpClientFactory httpClientFactory)
        {
            _xeroConfig = xeroConfig;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetLoginUriQueryResult> Handle(GetLoginUriQuery request, CancellationToken cancellationToken)
        {
            var client = new XeroClient(_xeroConfig.Value, _httpClientFactory.CreateClient());

            return await Task.Run(() => new GetLoginUriQueryResult(client.BuildLoginUri()), cancellationToken);
        }
    }
}
