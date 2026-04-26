using System.Net.Http;
using System.Threading.Tasks;
using SelfFinanceBlazorClient.Providers.Network;
using SelfFinanceBlazorClient.Providers.Network.Settings;

namespace SelfFinanceBlazorClient.Interfaces
{
    public interface INetworkProvider
    {
        public RequestResult<TResposeData> Get<TResposeData>(BaseNetworkSettings networkSettings, string url);
        public RequestResult Post(BaseNetworkSettings networkSettings, string url, HttpContent httpContent);
        public RequestResult Patch<TDataType>(BaseNetworkSettings networkSettings, string url, TDataType data);
        public RequestResult Delete(BaseNetworkSettings networkSettings, string url);
        public Task<RequestResult<TResposeData>> GetAsync<TResposeData>(BaseNetworkSettings networkSettings, string url);
        public Task<RequestResult> PostAsync(BaseNetworkSettings networkSettings, string url, HttpContent httpContent);
        public Task<RequestResult> PatchAsync<TDataType>(BaseNetworkSettings networkSettings, string url, TDataType data);
        public Task<RequestResult> DeleteAsync(BaseNetworkSettings networkSettings, string url);
    }
}
