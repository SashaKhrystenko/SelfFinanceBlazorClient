using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Task_12.Interfaces;
using Task_12.Providers.Network.Settings;

namespace Task_12.Providers.Network
{
    public class NetworkProvider : INetworkProvider
    {
        private readonly Dictionary<string, HttpClient> _httpClientDictionary;
        private readonly Dictionary<string, JwtSecurityToken> _jwtSecurityTokenDictionary;

        private readonly JwtSecurityTokenHandler _jwtHandler;

        public NetworkProvider()
        {
            _httpClientDictionary = new Dictionary<string, HttpClient>();
            _jwtSecurityTokenDictionary = new Dictionary<string, JwtSecurityToken>();

            _jwtHandler = new JwtSecurityTokenHandler();
        }

        public RequestResult<TResposeData> Get<TResposeData>(BaseNetworkSettings networkSettings, string url)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = GetOrCreateNewJwtToken(httpClient, networkSettings);

            if (jwtToken == null || DateTime.UtcNow >= jwtToken.ValidTo)
            {
                jwtToken = CreateNewToken(httpClient, networkSettings);
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = httpClient.GetAsync(url).Result;

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

        public RequestResult Post(BaseNetworkSettings networkSettings, string url, HttpContent httpContent)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

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

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = GetOrCreateNewJwtToken(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = httpClient.PostAsync(url, httpContent).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public RequestResult Delete(BaseNetworkSettings networkSettings, string url)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = GetOrCreateNewJwtToken(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = httpClient.DeleteAsync(url).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public RequestResult Patch<TDataType>(BaseNetworkSettings networkSettings, string url, TDataType data)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

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

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = GetOrCreateNewJwtToken(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = httpClient.PatchAsJsonAsync(url, data).Result;

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = httpResponse.Content.ReadAsStringAsync().Result
            };
        }

        public async Task<RequestResult<TResposeData>> GetAsync<TResposeData>(BaseNetworkSettings networkSettings, string url)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = await GetOrCreateNewJwtTokenAsync(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);

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

        public async Task<RequestResult> PostAsync(BaseNetworkSettings networkSettings, string url, HttpContent httpContent)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

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

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = await GetOrCreateNewJwtTokenAsync(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = await httpClient.PostAsync(url, httpContent);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<RequestResult> DeleteAsync(BaseNetworkSettings networkSettings, string url)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException($"{nameof(url)} is null or white space.", nameof(url));
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Relative))
            {
                throw new ArgumentException($"'{url}' is not url.", nameof(url));
            }

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = await GetOrCreateNewJwtTokenAsync(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(url);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        public async Task<RequestResult> PatchAsync<TDataType>(BaseNetworkSettings networkSettings, string url, TDataType data)
        {
            if (networkSettings == null)
            {
                throw new ArgumentNullException(nameof(networkSettings), $"{nameof(networkSettings)} is null.");
            }

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

            HttpClient httpClient = CreateOrGetHttpClientFromDictionary(networkSettings.BaseUrl);

            JwtSecurityToken jwtToken = await GetOrCreateNewJwtTokenAsync(httpClient, networkSettings);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);

            HttpResponseMessage httpResponse = await httpClient.PatchAsJsonAsync(url, data);

            return new RequestResult()
            {
                StatusCode = httpResponse.StatusCode,
                IsSuccess = httpResponse.IsSuccessStatusCode,
                Message = await httpResponse.Content.ReadAsStringAsync()
            };
        }

        private async Task<JwtSecurityToken> CreateNewTokenAsync(HttpClient httpClient, BaseNetworkSettings settings)
        {
            HttpResponseMessage httpResponse = await httpClient.PostAsJsonAsync(settings.AuthorizationRoute, settings.LoginRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                return _jwtHandler.ReadJwtToken(await httpResponse.Content.ReadAsStringAsync());
            }

            throw new Exception(await httpResponse.Content.ReadAsStringAsync());
        }

        private JwtSecurityToken CreateNewToken(HttpClient httpClient, BaseNetworkSettings settings)
        {
            HttpResponseMessage httpResponse = httpClient.PostAsJsonAsync(settings.AuthorizationRoute, settings.LoginRequest).Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                return _jwtHandler.ReadJwtToken(httpResponse.Content.ReadAsStringAsync().Result);
            }

            throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
        }

        private HttpClient CreateOrGetHttpClientFromDictionary(string baseUrl)
        {
            if (_httpClientDictionary.TryGetValue(baseUrl, out HttpClient httpClient))
            {
                return httpClient;
            }

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);

            _httpClientDictionary.Add(baseUrl, httpClient);

            return httpClient;
        }

        private JwtSecurityToken GetOrCreateNewJwtToken(HttpClient httpClient, BaseNetworkSettings networkSettings)
        {
            if (_jwtSecurityTokenDictionary.TryGetValue(networkSettings.BaseUrl, out JwtSecurityToken jwtToken))
            {
                if (jwtToken != null && DateTime.UtcNow < jwtToken.ValidTo)
                {
                    return jwtToken;
                }
                else
                {
                    _jwtSecurityTokenDictionary[networkSettings.BaseUrl] = CreateNewToken(httpClient, networkSettings);
                }
            }
            else
            {
                _jwtSecurityTokenDictionary.Add(networkSettings.BaseUrl, CreateNewToken(httpClient, networkSettings));
            }

            return _jwtSecurityTokenDictionary[networkSettings.BaseUrl];
        }

        private async Task<JwtSecurityToken> GetOrCreateNewJwtTokenAsync(HttpClient httpClient, BaseNetworkSettings networkSettings)
        {
            if (_jwtSecurityTokenDictionary.TryGetValue(networkSettings.BaseUrl, out JwtSecurityToken jwtToken))
            {
                if (jwtToken != null && DateTime.UtcNow < jwtToken.ValidTo)
                {
                    return jwtToken;
                }
                else
                {
                    _jwtSecurityTokenDictionary[networkSettings.BaseUrl] = await CreateNewTokenAsync(httpClient, networkSettings);
                }
            }
            else
            {
                _jwtSecurityTokenDictionary.Add(networkSettings.BaseUrl, await CreateNewTokenAsync(httpClient, networkSettings));
            }

            return _jwtSecurityTokenDictionary[networkSettings.BaseUrl];
        }
    }
}
