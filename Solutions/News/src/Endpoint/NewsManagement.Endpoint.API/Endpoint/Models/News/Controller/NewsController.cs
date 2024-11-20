namespace NewsManagement.Endpoint.News.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloudio.Web.Endpoint.API;
using Cloudio.Web.Core.Contract;
using NewsManagement.Core.News.Contracts;

[Route("api/[controller]")]
public class NewsController : AppController
{
    [HttpPost("register-news")]
    public async Task<IActionResult> Post(RegisterNewsCommand command, CancellationToken token)
    {
        var result = await CreateAsync<RegisterNewsCommand, RegisterNewsCommandResponse>(command, token);
        return result;
    }

    [HttpGet("get-news-detail/{id}")]
    public async Task<IActionResult> Get([FromRoute] long id, CancellationToken token)
    {
        var result = await ExcecuteQueryAsync<NewsDetailQuery, NewsDetailQueryResponse>(new NewsDetailQuery { Id = id }, token);
        return result;
    }

    [HttpGet("get-news-list")]
    public async Task<IActionResult> Get([FromBody] NewsListQuery query, CancellationToken token)
    {
        var result = await ExcecuteQueryAsync<NewsListQuery, PagedData<NewsListQueryResponse>>(query, token);
        return result;
    }
}