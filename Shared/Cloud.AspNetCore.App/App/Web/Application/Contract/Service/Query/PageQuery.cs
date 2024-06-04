namespace Cloud.Web.Core.Contract;

using Cloud.Core.Models;

public record PageQuery<D> : TransferModel, IPageQuery<D>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int SkipCount => (Page - 1) * PageSize;
    public string OrderBy { get; set; } = "Id";
    public bool Ascending { get; set; } = false;
    public bool NeedTotalCount { get; set; } = true;
}