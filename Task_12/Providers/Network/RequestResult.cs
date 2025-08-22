using System.Net;

namespace Task_12.Providers.Network
{
    public class RequestResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
