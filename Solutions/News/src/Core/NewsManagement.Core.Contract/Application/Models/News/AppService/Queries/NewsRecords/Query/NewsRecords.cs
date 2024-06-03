namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public record NewsRecords : PageQuery<PagedData<NewsRecordsResult>>
{ }