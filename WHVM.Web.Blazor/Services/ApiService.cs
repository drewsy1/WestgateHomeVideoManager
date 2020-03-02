using WHVM.Database.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WHVM.Web.Blazor.Services
{
    public class ApiService
    {
        public HttpClient _httpClient;

        public ApiService(HttpClient client)
        {
            _httpClient = client;
        }

        public static object DeserializeFromStream<t>(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<t>(jsonTextReader);
            }
        }

        public async Task<List<Clip>> GetClipsAsync()
        {
            var response = await _httpClient.GetAsync("api/Clips");
            response.EnsureSuccessStatusCode();

            await using var responseContent = await response.Content.ReadAsStreamAsync();
            return (List<Clip>)DeserializeFromStream<List<Clip>>(responseContent);
        }

        public async Task<Clip> GetClipByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Clips/{id}");
            response.EnsureSuccessStatusCode();

            await using var responseContent = await response.Content.ReadAsStreamAsync();
            return (Clip) DeserializeFromStream<Clip>(responseContent);
        }
    }
}