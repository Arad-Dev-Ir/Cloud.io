namespace KeywordsManagement.Endpoint.Keyword.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloudio.Web.Core.Contract;
using Cloudio.Web.Endpoint.API;
using KeywordsManagement.Core.Keyword.Contracts;

[Route("api/[controller]")]
public class KeywordsController : AppController
{
    [HttpPost("define-keyword")]
    public async Task<IActionResult> Post([FromBody] CreateKeywordCommand command, CancellationToken token)
    {
        var result = await CreateAsync<CreateKeywordCommand, CreateKeywordCommandResponse>(command, token);
        return result;
    }

    [HttpPut("edit-keyword-title")]
    public async Task<IActionResult> Put([FromBody] ChangeKeywordTitleCommand command, CancellationToken token)
    {
        var result = await EditAsync(command, token);
        return result;
    }

    [HttpPost("activate-keyword")]
    public async Task<IActionResult> Post([FromBody] ActivateKeywordCommand command, CancellationToken token)
    {
        var result = await EditAsync(command, token);
        return result;
    }

    [HttpPost("deactivate-keyword")]
    public async Task<IActionResult> Post([FromBody] DeactivateKeywordCommand command, CancellationToken token)
    {
        var result = await EditAsync(command, token);
        return result;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromBody] TitleAndStateSearchQuery command, CancellationToken token)
    {
        var result = await ExcecuteQueryAsync<TitleAndStateSearchQuery, PagedData<TitleAndStateSearchQueryResponse>>(command, token);
        return result;
    }
}