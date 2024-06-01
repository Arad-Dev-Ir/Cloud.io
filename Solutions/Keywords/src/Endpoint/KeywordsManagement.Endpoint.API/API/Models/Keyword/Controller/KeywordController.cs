namespace KeywordsManagement.Endpoint.Keyword.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloud.Web.Core.Contract;
using Cloud.Web.Endpoint.API;
using Core.Keyword.Contracts;

[Route("api/[controller]")]
public class KeywordController : ApiController
{
    [HttpPost("define-keyword")]
    public async Task<IActionResult> Post(CreateKeyword command)
    => await Create<CreateKeyword, long>(command);

    [HttpPut("edit-keyword-title")]
    public async Task<IActionResult> Put([FromBody] ChangeKeywordTitle command)
    => await Edit<ChangeKeywordTitle>(command);

    [HttpPost("activate-keyword")]
    public async Task<IActionResult> Post([FromBody] ActivateKeyword command)
    => await Edit<ActivateKeyword>(command);

    [HttpPost("deactivate-keyword")]
    public async Task<IActionResult> Post([FromBody] DeactivateKeyword command)
    => await Edit<DeactivateKeyword>(command);

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] TitleAndModeSearch command)
    => await ExcecuteQuery<TitleAndModeSearch, PagedData<TitleAndModeSearchResult>>(command);
}
