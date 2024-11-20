namespace Cloudio.Client.Endpoints.News;

using Cloudio.Core.Services.Serialization;

public class NewsClient : IHttpTypedClient
{
    private const string BaseAddress = "https://localhost:8001/nm/api/news/";

    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public NewsClient(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new(BaseAddress);

        _jsonSerializer = jsonSerializer;
    }

    public async Task<IResult> PostAsync(RegisterNewsCommand request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.PostAsJsonAsync("register-news", request, token);
        var content = await response.Content.ReadAsStringAsync(token);

        if (!response.IsSuccessStatusCode)
        {
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            var data = _jsonSerializer.Deserialize<RegisterNewsCommandResponse>(content)!;
            result = Results.Json(data, statusCode: StatusCode.Created);
        }

        return result;
    }

    public async Task<IResult> GetAsync(NewsDetailQuery request, CancellationToken token)
    {
        var data = await _httpClient.GetFromJsonAsync<NewsDetailQueryResponse>($"get-news-detail/{request.Id}", token);

        var result = Results.Json(data, statusCode: StatusCode.Ok);
        return result;
    }

    public async Task<IResult> GetAllAsync(NewsListQuery request, CancellationToken token)
    {
        IResult? result;

        var response = await _httpClient.GetFromJsonAsync("get-news-list", request, token: token);
        var content = await response.Content.ReadAsStringAsync(token);

        if (!response.IsSuccessStatusCode)
        {
            var data = _jsonSerializer.Deserialize<ErrorDetails>(content)!;
            result = Results.Json(data, statusCode: data.Status);
        }
        else
        {
            var data = _jsonSerializer.Deserialize<PagedData<NewsListQueryResponse>>(content)!;
            result = Results.Json(data, statusCode: StatusCode.Ok);
        }

        return result;
    }
}