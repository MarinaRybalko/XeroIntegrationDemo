using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Xero.NetStandard.OAuth2.Config;
using XeroDemoApp.Domain.Commands;
using XeroDemoApp.Domain.Queries;
using XeroDemoApp.Domain.Queries.Results;

namespace XeroDemoApp.Controllers
{
    [ApiController]
    [Route("api/xero/auth")]
    public class XeroAuthController : ControllerBase
    {
        private readonly IMediator _bus;

        public XeroAuthController(IMediator bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Builds Login Uri for user based on <see cref="XeroConfiguration"></see>
        /// </summary>
        /// <returns>Login uri</returns>
        [HttpGet]
        [Route("login-uri")]
        public async Task<GetLoginUriQueryResult> GetLoginUri()
        {
            return await _bus.Send(GetLoginUriQuery.Create());
        }

        /// <summary>
        /// Gets connection 
        /// </summary>
        /// <returns>Connection if exists else null</returns>
        [HttpGet]
        [Route("connection")]
        public async Task<GetConnectionQueryResult> GetConnection()
        {
            return await _bus.Send(GetConnectionQuery.Create());
        }

        /// <summary>
        /// Creates and saves access token 
        /// </summary>
        /// <param name="code">The authorization code received in the callback</param>
        /// <param name="state">State from <see cref="XeroConfiguration"></see></param>
        /// <returns></returns>
        [HttpGet]
        [Route("callback")]
        public async Task<ActionResult> Callback(string code, string state)
        {
            await _bus.Send(CreateAccessTokenCommand.Create(code, state));
            return Redirect("/");
        }

        /// <summary>
        /// Deletes connection from Xero and removes token 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("disconnect")]
        public async Task<IActionResult> Disconnect()
        {
            var result = await _bus.Send(DisconnectCommand.Create());
            if (result) return Ok();
            return BadRequest(result.FailureReason);
        }
    }
}
