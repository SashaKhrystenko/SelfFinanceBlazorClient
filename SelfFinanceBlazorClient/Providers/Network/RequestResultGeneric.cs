namespace SelfFinanceBlazorClient.Providers.Network
{
    public class RequestResult<TResponseData> : RequestResult
    {
        public TResponseData ResponseData { get; set; }
    }
}
