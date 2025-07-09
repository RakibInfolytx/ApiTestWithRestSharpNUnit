using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestSharpDemo
{
    public class ApiClient
    {
        private readonly RestClient _client;
        private readonly AppConfig _config;
        private string? _accessToken;

        private const string AuthEndpoint = "api/1.0/oauth2/access_token";
        private const string GetProductEndpoint = "fbu/uapi/modules/68360dad5741f8cec7e82844/api";

        public ApiClient()
        {
            _config = AppConfig.LoadFromFile();
            _client = new RestClient(_config.BaseUrl);
        }

        /// <summary>
        /// Authenticate using client credentials and store access token.
        /// </summary>
        public async Task<bool> AuthenticateAsync()
        {
            var request = new RestRequest(AuthEndpoint, Method.Post);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", $"Basic {_config.GetEncodedAuthHeader()}");

            // Adding parameters as form data
            request.AddParameter("client_id", _config.ClientId);
            request.AddParameter("client_secret", _config.ClientSecret);
            request.AddParameter("grant_type", _config.GrantType);

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                try
                {
                    using var json = JsonDocument.Parse(response.Content);
                    _accessToken = json.RootElement.GetProperty("access_token").GetString();
                    return !string.IsNullOrEmpty(_accessToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing authentication response: {ex.Message}");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"Authentication failed. Status: {response.StatusCode}, Content: {response.Content}");
                return false;
            }
        }

        /// <summary>
        /// Calls the Module API with required payload.
        /// </summary>
        public async Task<bool> GetProductListAsync()
        {
            var request = CreateAuthorizedRequest(GetProductEndpoint, Method.Get);

            var payload = new
            {
                productCode = "CPM",
                languageIsoCode = "en",
                territoryIsoCode = "CA"
            };

            request.AddJsonBody(payload);

            var response = await _client.ExecuteAsync(request);
            // Console.WriteLine("📄 Module API Response:\n" + response.Content);

            return response.IsSuccessful && !string.IsNullOrEmpty(response.Content);
        }

        
        public async Task<RestResponse> GetProductManifestAsync()
        {
            var request = CreateAuthorizedRequest(GetProductEndpoint, Method.Get);

            var payload = new
            {
                productId = "99c36438-317b-11f0-b8a4-78586e2af1e3"
            };

            request.AddJsonBody(payload);

            return await _client.ExecuteAsync(request);
        }

        
        private RestRequest CreateAuthorizedRequest(string endpoint, Method method)
        {
            var request = new RestRequest(endpoint, method);

            if (!string.IsNullOrEmpty(_accessToken))
            {
                request.AddHeader("Authorization", $"Bearer {_accessToken}");
            }
            else
            {
                Console.WriteLine("Warning: Access token is null or empty when creating authorized request.");
            }

            return request;
        }
    }
}
