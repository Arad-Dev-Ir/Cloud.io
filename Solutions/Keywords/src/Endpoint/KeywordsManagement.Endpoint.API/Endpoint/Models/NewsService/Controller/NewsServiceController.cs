namespace KeywordsManagement.Endpoint.Service.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloudio.Web.Endpoint.API;
using KeywordsManagement.Core.NewsService.Contracts;

[Route("api/[controller]")]
public class NewsServiceController : AppController
{
    [HttpPost("create-new-service")]
    public async Task<IActionResult> Post(CreateNewsServiceCommand command, CancellationToken token)
    {
        var result = await CreateAsync<CreateNewsServiceCommand, CreateNewsServiceCommandResponse>(command, token);
        return Ok(result);
    }
}