using MediatR;
using XeroDemoApp.Domain.Queries.Results;

namespace XeroDemoApp.Domain.Queries
{
    public class GetConnectionQuery : IRequest<GetConnectionQueryResult>
    {
        public static GetConnectionQuery Create() => new GetConnectionQuery();
    }
}
