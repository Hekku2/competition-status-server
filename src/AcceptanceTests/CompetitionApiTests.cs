using System;
using System.Threading.Tasks;
using Api.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace AcceptanceTests
{
    public class CompetitionApiTests
    {
        private RestClient _client;
        private const string ApiName = "Competition";

        [SetUp]
        public void Setup()
        {
            var uri = Environment.GetEnvironmentVariable("APP_URI");
            _client = new RestClient($"{uri}{ApiName}");
        }

        [Test]
        public async Task CurrentCompetitor_IsNullAtStart()
        {
            var request = new RestRequest("current-competitor");
            var response = await _client.GetAsync<CurrentCompetitorEnvelopeModel>(request, System.Threading.CancellationToken.None);
            response.Type.Should().Be("current-competitor");
            response.Version.Should().Be("1");
            response.Content.Should().BeNull();
        }
    }
}
