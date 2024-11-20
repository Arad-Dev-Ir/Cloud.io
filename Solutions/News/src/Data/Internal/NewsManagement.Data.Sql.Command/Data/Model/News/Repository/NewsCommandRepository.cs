namespace NewsManagement.Data.Sql.News.Commands;

using Cloudio.Web.Data.Sql.Command;
using Core.News.Contracts;
using NewsManagement.Core.News.Models;
using NewsManagement.Data.Sql.Commands;

public sealed class NewsCommandRepository(NewsManagementCommandContext context)
    : CommandRepository<NewsManagementCommandContext, News>(context), INewsCommandRepository
{ }