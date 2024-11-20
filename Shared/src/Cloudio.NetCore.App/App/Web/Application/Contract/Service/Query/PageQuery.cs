namespace Cloudio.Web.Core.Contract;

using Cloudio.Core.Models;

public record PageQuery<TOutput> : DataTransferObject, IPageQuery<TOutput>
{
    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public int SkipCount => (Page - 1) * PageSize;

    public string OrderBy { get; init; } = "Id";

    public bool Ascending { get; init; } = false;

    public bool NeedTotalCount { get; init; } = true;
}