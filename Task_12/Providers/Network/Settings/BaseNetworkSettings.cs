namespace Task_12.Providers.Network.Settings
{
    public abstract class BaseNetworkSettings
    {
        public LoginRequestModel LoginRequest { get; set; }
        public string BaseUrl { get; set; }
        public string AuthorizationRoute { get; set; }
    }
}
