namespace Cloudio.Client.Endpoints.Keywords;

using Microsoft.AspNetCore.Mvc;

public class KeywordsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("/keywords/");
        mapGroup.MapPost("define-keyword",
        async ([FromBody] CreateKeywordCommand request, KeywordsClient keywordsClient, CancellationToken token) =>
        {
            return await keywordsClient.PostAsync(request, token);
        })
        .WithTags(KeywordsTags.Define);

        mapGroup.MapPut("edit-keyword-title",
        async ([FromBody] ChangeKeywordTitleCommand request, KeywordsClient keywordsClient, CancellationToken token) =>
        {
            return await keywordsClient.PutAsync(request, token);
        })
       .WithTags(KeywordsTags.Define);

        mapGroup.MapPost("activate-keyword",
        async ([FromBody] ActivateKeywordCommand request, KeywordsClient keywordsClient, CancellationToken token) =>
        {
            return await keywordsClient.PostAsync(request, token);
        })
        .WithTags(KeywordsTags.Activate);

        mapGroup.MapPost("deactivate-keyword",
        async ([FromBody] DeactivateKeywordCommand request, KeywordsClient keywordsClient, CancellationToken token) =>
        {
            return await keywordsClient.PostAsync(request, token);
        })
        .WithTags(KeywordsTags.Deactivate);

        mapGroup.MapGet("search",
        async ([FromBody] TitleAndStateSearchQuery request, KeywordsClient keywordsClient, CancellationToken token) =>
        {
            return await keywordsClient.SearchAsync(request, token);
        })
       .WithTags(KeywordsTags.Search);
    }
}