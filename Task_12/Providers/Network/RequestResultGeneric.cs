namespace Task_12.Providers.Network
{
    public class RequestResult<TResponseData> : RequestResult
    {
        public TResponseData ResponseData { get; set; }
    }
}
