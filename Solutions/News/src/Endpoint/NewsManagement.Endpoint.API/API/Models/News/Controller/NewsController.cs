namespace NewsManagement.Endpoint.News.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloud.Web.Endpoint.API;
using Cloud.Web.Core.Contract;
using Core.News.Contracts;

[Route("api/[controller]")]
public class NewsController : ApiController
{
    [HttpPost("register-news")]
    public async Task<IActionResult> Post(RegisterNews command, CancellationToken cancellationToken)
    => await Create<RegisterNews, long>(command, cancellationToken);

    [HttpGet("get-news-detail/{id}")]
    public async Task<IActionResult> Get([FromRoute] long id, CancellationToken cancellationToken)
    => await ExcecuteQuery<NewsDetail, NewsDetailResult>(new NewsDetail { Id = id }, cancellationToken);

    [HttpGet("get-news-list")]
    public async Task<IActionResult> Get([FromQuery] NewsRecords query, CancellationToken cancellationToken)
    => await ExcecuteQuery<NewsRecords, PagedData<NewsRecordsResult>>(query, cancellationToken);
}