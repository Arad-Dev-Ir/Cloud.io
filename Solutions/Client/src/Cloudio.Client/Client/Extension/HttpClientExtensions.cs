namespace Cloudio.Client;

internal static class HttpClientExtensions
{
    private static async Task<TOutput?> SendAsJsonAsync<TValue, TOutput>
    (this HttpClient httpClient, string requestUri, HttpMethod method, TValue value, UriKind uriKind = UriKind.Absolute, CancellationToken token = default)
    {
        TOutput? result = default!;

        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(value),
            Method = method,
            RequestUri = new Uri($"{httpClient.BaseAddress}{requestUri}", uriKind)
        };
        var response = await httpClient.SendAsync(request, token);
        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadFromJsonAsync<TOutput>(cancellationToken: token);
        }

        return result;
    }

    public static async Task<TOutput?> GetFromJsonAsync<TValue, TOutput>
    (this HttpClient httpClient, string requestUri, TValue value, UriKind uriKind = UriKind.Absolute, CancellationToken token = default)
    {
        var result = await httpClient.SendAsJsonAsync<TValue, TOutput>(requestUri, HttpMethod.Get, value, uriKind, token);
        return result;
    }

    private static async Task<HttpResponseMessage> SendAsJsonAsync<TValue>
    (this HttpClient httpClient, string requestUri, HttpMethod method, TValue value, UriKind uriKind = UriKind.Absolute, CancellationToken token = default)
    {
        var request = new HttpRequestMessage
        {
            Content = JsonContent.Create(value),
            Method = method,
            RequestUri = new Uri($"{httpClient.BaseAddress}{requestUri}", uriKind)
        };

        var result = await httpClient.SendAsync(request, token);
        return result;
    }

    public static async Task<HttpResponseMessage> GetFromJsonAsync<TValue>
    (this HttpClient httpClient, string requestUri, TValue value, UriKind uriKind = UriKind.Absolute, CancellationToken token = default)
    {
        var result = await httpClient.SendAsJsonAsync<TValue>(requestUri, HttpMethod.Get, value, uriKind, token);
        return result;
    }
}