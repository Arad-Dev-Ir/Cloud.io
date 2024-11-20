namespace Cloudio.Client.Endpoints;

using static Math;
using Cloudio.Core.Models;

public record PagedData<TOutput> : DataTransferObject where TOutput : DataTransferObject
{
    public List<TOutput> Items { get; init; } = [];
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage => (Page * PageSize) < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public int TotalPages => (int)Ceiling(TotalCount / (double)PageSize);
}
