namespace Cloud.Web.Core.Contract;

public interface IPageQuery<D> : IQuery<D>
{
    int Page { get; }
    int PageSize { get; }
    int SkipCount { get; }
    string OrderBy { get; }
    bool Ascending { get; }
    bool NeedTotalCount { get; }
}