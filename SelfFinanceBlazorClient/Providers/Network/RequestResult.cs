using System.Net;

namespace SelfFinanceBlazorClient.Providers.Network
{
    public class RequestResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
