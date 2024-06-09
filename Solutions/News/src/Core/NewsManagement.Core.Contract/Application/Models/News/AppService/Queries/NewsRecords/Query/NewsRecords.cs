namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public sealed record NewsRecords : PageQuery<PagedData<NewsRecordsResult>>
{ }