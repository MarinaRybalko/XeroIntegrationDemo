namespace XeroDemoApp.Domain.Queries.Results
{
    public class GetLoginUriQueryResult
    {
        public string Uri { get; set; }

        public GetLoginUriQueryResult(string uri)
        {
            Uri = uri;
        }
    }
}
