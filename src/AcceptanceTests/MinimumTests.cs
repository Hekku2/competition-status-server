using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Org.OpenAPITools.Api;

namespace AcceptanceTests
{
    public class MinimumTests
    {
        private HomeApi _client;

        [SetUp]
        public void Setup()
        {
            var uri = Environment.GetEnvironmentVariable("APP_URI");
            if (string.IsNullOrWhiteSpace(uri))
            {
                Assert.Inconclusive("URI not set. Unable to execute acceptance tests.");
            }

            _client = new HomeApi(uri);
        }

        [Test]
        public async Task GetHealth_ReturnsOk()
        {
            await _client.HomeGetHealthAsync();
        }
    }
}
