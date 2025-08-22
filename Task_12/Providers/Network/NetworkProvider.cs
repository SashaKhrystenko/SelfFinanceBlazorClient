using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Task_12.Interfaces;

namespace Task_12.Providers.Network
{
    public class NetworkProvider : INetworkProvider
    {
        private readonly HttpClient _httpClient;
        private readonly JwtSecurityTokenHandler _jwtHandler;

        private JwtSecurityToken _jwtToken;

        public NetworkProvider(HttpClient httpClent)
        {
            if (httpClent == null)
            {
                throw new ArgumentNullException(nameof(httpClent), $"{nameof(httpClent)} is null.");
            }

            _httpClient = httpClent;

            _jwtHandler = new JwtSecurityTokenHandler();
        }

        public RequestResult<TResposeData> Get<TResposeData>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = GetNewToken();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = _httpClient.GetAsync(url).Result;

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RequestResult<TResposeData>()
                {
                    StatusCode = httpResponse.StatusCode,
                    IsSuccess = httpResponse.IsSuccessStatusCode,
                    Message = httpResponse.Content.ReadAsStringAsync().Result,
                    ResponseData = default
                };
            }

            TResposeData resposeData = httpResponse.Content.ReadFromJsonAsync<TResposeData>().Result;

            return new RequestResult<TResposeData>()
            {
                StatusCode = httpResponse.StatusCode,
                Message = string.Empty,
                ResponseData = resposeData
            };
        }

        public RequestResult Post(string url, HttpContent httpContent)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (httpContent == null)
            {
                throw new ArgumentNullException(nameof(httpContent), $"{nameof(httpContent)} is null.");
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = GetNewToken();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = _httpClient.PostAsync(url, httpContent).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public RequestResult Delete(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = GetNewTokenAsync().Result;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = _httpClient.DeleteAsync(url).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public RequestResult Patch<TDataType>(string url, TDataType data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"{nameof(data)} is null.");
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = GetNewToken();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = _httpClient.PatchAsJsonAsync(url, data).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public async Task<RequestResult<TResposeData>> GetAsync<TResposeData>(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = await GetNewTokenAsync();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return new RequestResult<TResposeData>()
                {
                    StatusCode = httpResponse.StatusCode,
                    IsSuccess = httpResponse.IsSuccessStatusCode,
                    Message = await httpResponse.Content.ReadAsStringAsync(),
                    ResponseData = default
                };
            }

            TResposeData resposeData = await httpResponse.Content.ReadFromJsonAsync<TResposeData>();

            return new RequestResult<TResposeData>()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = string.Empty,
                ResponseData = resposeData
            };
        }

        public async Task<RequestResult> PostAsync(string url, HttpContent httpContent)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (httpContent == null)
            {
                throw new ArgumentNullException(nameof(httpContent), $"{nameof(httpContent)} is null.");
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = await GetNewTokenAsync();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = await _httpClient.PostAsync(url, httpContent);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<RequestResult> DeleteAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = GetNewTokenAsync().Result;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(url);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<RequestResult> PatchAsync<TDataType>(string url, TDataType data)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"{nameof(data)} is null.");
            }

            if (_jwtToken == null || DateTime.UtcNow >= _jwtToken.ValidTo)
            {
                _jwtToken = await GetNewTokenAsync();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken.RawData);

            HttpResponseMessage httpResponse = await _httpClient.PatchAsJsonAsync(url, data);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        private async Task<JwtSecurityToken> GetNewTokenAsync()
        {
            HttpResponseMessage httpResponse = await _httpClient.GetAsync("/login?clientName=admin&password=123");

            if (httpResponse.IsSuccessStatusCode)
            {
                return _jwtHandler.ReadJwtToken(await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }

        private JwtSecurityToken GetNewToken()
        {
            HttpResponseMessage httpResponse = _httpClient.GetAsync("/login?clientName=admin&password=123").Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                return _jwtHandler.ReadJwtToken(httpResponse.Content.ReadAsStringAsync().Result);
            }

            throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
        }
    }
}
