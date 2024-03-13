using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using HHGlobal.Features.Tax.Calculate;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;

namespace HHGlobal.Tests.Tax.Calculate;

[TestFixture]
public class CalculateTaxForJobsTests
{
    private  WebApplicationFactory<Program> _factory;

    [OneTimeSetUp]
    public void Init()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Test]
    public async Task CalculateTaxForJobs_ReturnsCorrectData_Integration()
    {
        var request = await File.ReadAllTextAsync("./Tax/Calculate/Json/Request.json");

        var requestParsed = JsonSerializer.Deserialize<CalculateTaxForJobs.RequestBody>(request);
        
        using var client = _factory.CreateClient(new() { BaseAddress = new Uri("http://localhost:5139/api/")});
        var response = await client.PostAsJsonAsync("CalculateTaxForJobs",requestParsed);

        var responseParsed = JToken.Parse(await response.Content.ReadAsStringAsync());
        var expected =
            JToken.Parse(
                await File.ReadAllTextAsync("./Tax/Calculate/Json/Response.json"));

        responseParsed.Should().BeEquivalentTo(expected);
    }
}