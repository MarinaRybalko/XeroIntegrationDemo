using MediatR;
using XeroDemoApp.Domain.Queries.Results;

namespace XeroDemoApp.Domain.Queries
{
    public class GetLoginUriQuery : IRequest<GetLoginUriQueryResult>
    { 
        public static GetLoginUriQuery Create() => new GetLoginUriQuery();
    }
}
