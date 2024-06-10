namespace KeywordsManagement.Endpoint.Keyword.APIs;

using Microsoft.AspNetCore.Mvc;
using Cloud.Web.Core.Contract;
using Cloud.Web.Endpoint.API;
using Core.Keyword.Contracts;

[Route("api/[controller]")]
public class KeywordController : ApiController
{
    [HttpPost("define-keyword")]
    public async Task<IActionResult> Post(CreateKeyword command, CancellationToken cancellationToken)
    => await Create<CreateKeyword, long>(command, cancellationToken);

    [HttpPut("edit-keyword-title")]
    public async Task<IActionResult> Put([FromBody] ChangeKeywordTitle command, CancellationToken cancellationToken)
    => await Edit<ChangeKeywordTitle>(command, cancellationToken);

    [HttpPost("activate-keyword")]
    public async Task<IActionResult> Post([FromBody] ActivateKeyword command, CancellationToken cancellationToken)
    => await Edit<ActivateKeyword>(command, cancellationToken);

    [HttpPost("deactivate-keyword")]
    public async Task<IActionResult> Post([FromBody] DeactivateKeyword command, CancellationToken cancellationToken)
    => await Edit<DeactivateKeyword>(command, cancellationToken);

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] TitleAndModeSearch command, CancellationToken cancellationToken)
    => await ExcecuteQuery<TitleAndModeSearch, PagedData<TitleAndModeSearchResult>>(command, cancellationToken);
}
