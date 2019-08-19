using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace WHVM.WebAPI.Tests
{
    public class WebAPITests
    {
        private readonly HttpClient _client;

        protected WebAPITests()
        {
            var webHost = Program.CreateWebHostBuilder(new string[0])
                .UseEnvironment("Development");
            var server = new TestServer(webHost);
            _client = server.CreateClient();
        }

        protected async Task<HttpResponseMessage> ApiGet(string apiMethod, string apiArgs = "")
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), "/api/"+apiMethod+"/"+apiArgs);
            Task<HttpResponseMessage> response = _client.SendAsync(request);
            request.Dispose();

            return await response;
        }
    }
}
