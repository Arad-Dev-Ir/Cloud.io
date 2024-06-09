namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.News.Contracts;
using Sql.Commands;
using Core.News.Models;

public sealed class NewsCommandRepository(NewsManagementCommandContext context) :
    CommandRepository<NewsManagementCommandContext, News>(context), INewsCommandRepository
{ }