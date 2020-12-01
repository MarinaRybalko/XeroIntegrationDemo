using XeroDemoApp.Domain.Entities.Enums;

namespace XeroDemoApp.Domain.Queries.Results
{
    public class GetConnectionQueryResult
    {
        public ConnectionState State { get; set; }

        public GetConnectionQueryResult(ConnectionState state)
        {
            State = state;
        }
    }
}
