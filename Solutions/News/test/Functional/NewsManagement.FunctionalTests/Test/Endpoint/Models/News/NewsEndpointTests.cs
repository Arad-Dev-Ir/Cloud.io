namespace NewsManagement.Test.News.Functionals;

using System;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http;
using Cloudio.Core;
using NewsManagement.Core.News.Contracts;

public class NewsEndpointTests : IClassFixture<HostFixture>
{
    private readonly HttpClient _httpClient;
    private readonly HostFixture _hostFixture;

    public NewsEndpointTests(HostFixture fixture)
    {
        _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7011/api/news/") };
        _hostFixture = fixture;
    }

    [Fact]
    public async void Register_news_should_return_ok_status()
    {
        // setup
        var request = new RegisterNewsCommand
        {
            Title = UniqueIdentifier.GetIdAsString(),
            Body = "Body",
            Description = "Description",
            KeywordsCodes = ["0A0AF0F6-858C-408C-BCDA-3767F74A357D"]
        };

        // exercise
        var response = await _httpClient.PostAsJsonAsync("register-news", request);

        // verify
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}