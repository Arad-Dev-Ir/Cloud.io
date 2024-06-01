namespace KeywordsManagement.Endpoint.Service.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloud.Web.Endpoint.API;
using Core.NewsService.Contracts;

[Route("api/[controller]")]
public class NewsServiceController : ApiController
{
    [HttpPost("create-new-service")]
    public async Task<IActionResult> Post(CreateNewsService command)
    => await Create<CreateNewsService, long>(command);
}