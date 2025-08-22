using System;

namespace Task_12.Providers.Network
{
    public class NetworkSetting
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        public DateTime ExpireTokenTime { get; set; }
    }
}
