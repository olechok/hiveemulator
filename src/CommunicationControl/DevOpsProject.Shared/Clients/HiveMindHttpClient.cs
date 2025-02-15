using DevOpsProject.Shared.Models;
using System.Text;
using System.Text.Json;

namespace DevOpsProject.Shared.Clients
{
    public class HiveMindHttpClient
    {
        private readonly HttpClient _httpClient;

        public HiveMindHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> SendCommunicationControlConnectAsync(string requestSchema, string ip, int port, HiveConnectRequest payload)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = requestSchema,
                Host = ip,
                Port = port,
                Path = "api/hive/connect"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uriBuilder.Uri, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string?> SendCommunicationControlTelemetryAsync(string requestSchema, string ip, int port, HiveTelemetryRequest payload)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = requestSchema,
                Host = ip,
                Port = port,
                Path = "api/hive/telemetry"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uriBuilder.Uri, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
