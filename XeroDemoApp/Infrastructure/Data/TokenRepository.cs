using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xero.NetStandard.OAuth2.Token;
using XeroDemoApp.Infrastructure.Data.Abstractions;

namespace XeroDemoApp.Infrastructure.Data
{
    public class TokenRepository : ITokenRepository
    {
        private static readonly string XeroTokenPath = "./xerotoken.json";

        public async Task Create(XeroOAuth2Token xeroToken)
        {
            await using var fs = File.Create(XeroTokenPath);
            await JsonSerializer.SerializeAsync(fs, xeroToken);
        }

        public async Task<XeroOAuth2Token> Get()
        {
            if (!File.Exists(XeroTokenPath)) return default;

            await using var fs = File.OpenRead(XeroTokenPath);
            return await JsonSerializer.DeserializeAsync<XeroOAuth2Token>(fs);
        }

        public async Task Delete()
        {
            await Task.Run(() => File.Delete(XeroTokenPath));
        }
    }
}
