using DevOpsProject.Shared.Models;
using System.Text;
using System.Text.Json;

namespace DevOpsProject.Shared.Clients
{
    public class CommunicationControlHttpClient
    {
        private readonly HttpClient _httpClient;

        public CommunicationControlHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendHiveControlCommandAsync(string ip, int port, MoveHiveMindCommand command)
        {
            var uriBuilder = new UriBuilder
            {
                // TODO: IMPORTANT - MOVE REQUEST SCHEMA TO CONFIG!!!
                Scheme = "http",
                Host = ip,
                Port = port,
                Path = "api/command"
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uriBuilder.Uri, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
