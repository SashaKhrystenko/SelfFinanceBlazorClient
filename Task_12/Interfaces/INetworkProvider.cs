using System.Net.Http;
using System.Threading.Tasks;
using Task_12.Providers.Network;

namespace Task_12.Interfaces
{
    public interface INetworkProvider
    {
        public RequestResult<TResposeData> Get<TResposeData>(string url);
        public RequestResult Post(string url, HttpContent httpContent);
        public RequestResult Patch<TDataType>(string url, TDataType data);
        public RequestResult Delete(string url);
        public Task<RequestResult<TResposeData>> GetAsync<TResposeData>(string url);
        public Task<RequestResult> PostAsync(string url, HttpContent httpContent);
        public Task<RequestResult> PatchAsync<TDataType>(string url, TDataType data);
        public Task<RequestResult> DeleteAsync(string url);
    }
}
