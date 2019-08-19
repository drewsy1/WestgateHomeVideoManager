using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WHVM.WebAPI.Tests.Tests
{
    public class Sources : WebAPITests
    {
        [Fact]
        public async Task SourcesGetAll()
        {
            HttpResponseMessage response = await ApiGet("sources");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.Dispose();
        }

        [Theory]
        [InlineData(1)]
        public async Task SourcesGetSingle(int id)
        {
            HttpResponseMessage response = await ApiGet("sources", id.ToString());
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response.Dispose();
        }
    }
}
