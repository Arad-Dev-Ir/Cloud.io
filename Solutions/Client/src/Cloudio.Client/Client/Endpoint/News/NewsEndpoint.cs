namespace Cloudio.Client.Endpoints.News;

using Microsoft.AspNetCore.Mvc;

public class NewsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("/news/");
        mapGroup.MapPost("register-news",
        async ([FromBody] RegisterNewsCommand request, NewsClient newsClient, CancellationToken token) =>
        {
            return await newsClient.PostAsync(request, token);
        })
        .WithTags(NewsTags.Register);

        mapGroup.MapGet("get-news-detail/{id}",
        async ([AsParameters] NewsDetailQuery request, NewsClient newsClient, CancellationToken token) =>
        {
            return await newsClient.GetAsync(request, token);
        })
        .WithTags(NewsTags.GetDetail);

        mapGroup.MapGet("get-news-list",
        async ([FromBody] NewsListQuery request, NewsClient newsClient, CancellationToken token) =>
        {
            return await newsClient.GetAllAsync(request, token);
        })
        .WithTags(NewsTags.GetAll);
    }
}