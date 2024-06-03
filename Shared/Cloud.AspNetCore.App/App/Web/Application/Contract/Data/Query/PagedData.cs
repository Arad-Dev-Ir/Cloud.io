namespace Cloud.Web.Core.Contract;

using static Math;
using Cloud.Core.Models;

public record PagedData<T> where T : TransferModel, new()
{
    public List<T> Records { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNextPage => (Page * PageSize) < TotalCount;
    public bool HasPreviousPage => PageSize > 1;
    public int TotalPages => (int)Ceiling(TotalCount / (double)PageSize);
}