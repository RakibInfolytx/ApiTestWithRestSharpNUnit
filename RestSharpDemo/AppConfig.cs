using System.Text;
using System.Text.Json;

namespace RestSharpDemo
{
    public class AppConfig
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string GrantType { get; set; } = "client_credentials";

        public string GetEncodedAuthHeader()
        {
            var raw = $"{ClientId}:{ClientSecret}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
        }

        public static AppConfig LoadFromFile(string path = "appsettings.json")
        {
            var json = File.ReadAllText(path);
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement.GetProperty("ApiSettings");

            return new AppConfig
            {
                BaseUrl = root.GetProperty("BaseUrl").GetString() ?? "",
                ClientId = root.GetProperty("ClientId").GetString() ?? "",
                ClientSecret = root.GetProperty("ClientSecret").GetString() ?? "",
                GrantType = root.GetProperty("GrantType").GetString() ?? "client_credentials"
            };
        }
    }
}
