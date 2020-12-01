using System.Threading.Tasks;
using Xero.NetStandard.OAuth2.Token;

namespace XeroDemoApp.Infrastructure.Data.Abstractions
{
    public interface ITokenRepository
    {
        Task Create(XeroOAuth2Token xeroToken);
        Task<XeroOAuth2Token> Get();
        Task Delete();
    }
}
