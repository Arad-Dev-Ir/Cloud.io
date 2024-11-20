namespace Cloudio.Client.Endpoints.Keywords;

using Cloudio.Core.Services.Serialization;

public class KeywordsClient : IHttpTypedClient
{
    private const string BaseAddress = "https://localhost:8001/km/api/keywords/";

    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public KeywordsClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new(BaseAddress);

        _jsonSerializer = jsonSerializer;
    }

    public async Task<IResult> PostAsync(CreateKeywordCommand request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.PostAsJsonAsync("define-keyword", request, token);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(token);
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync(token);
            var data = _jsonSerializer.Deserialize<CreateKeywordCommandResponse>(content)!;
            result = Results.Json(data, statusCode: StatusCode.Created);
        }

        return result;
    }

    public async Task<IResult> PutAsync(ChangeKeywordTitleCommand request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.PutAsJsonAsync("edit-keyword-title", request, token);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(token);
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            result = Results.Ok();
        }

        return result;
    }

    public async Task<IResult> SearchAsync(TitleAndStateSearchQuery request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.GetFromJsonAsync("search", request, token: token);
        var content = await response.Content.ReadAsStringAsync(token);

        if (!response.IsSuccessStatusCode)
        {
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            var data = _jsonSerializer.Deserialize<PagedData<TitleAndStateSearchQueryResponse>>(content)!;
            result = Results.Json(data, statusCode: StatusCode.Ok);
        }

        return result;
    }

    public async Task<IResult> PostAsync(ActivateKeywordCommand request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.PostAsJsonAsync("activate-keyword", request, token);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(token);
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            result = Results.Ok();
        }

        return result;
    }

    public async Task<IResult> PostAsync(DeactivateKeywordCommand request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.PostAsJsonAsync("deactivate-keyword", request, token);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(token);
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            result = Results.Ok();
        }

        return result;
    }
}