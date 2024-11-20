namespace Cloudio.Web.Core.Contract;

public interface IPageQuery<TOutput> : IRequest<TOutput>
{
    int Page { get; }

    int PageSize { get; }

    int SkipCount { get; }

    string OrderBy { get; }

    bool Ascending { get; }

    bool NeedTotalCount { get; }
}