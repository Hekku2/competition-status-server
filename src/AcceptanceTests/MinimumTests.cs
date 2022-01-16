using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace AcceptanceTests
{
    public class MinimumTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            var uri = Environment.GetEnvironmentVariable("APP_URI");
            _client = new RestClient(uri);
        }

        [Test]
        public async Task GetHealth_ReturnsOk()
        {
            var request = new RestRequest();
            var response = await _client.GetAsync(request, System.Threading.CancellationToken.None);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
