namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.NewsService.Contracts;
using Sql.Commands;
using Core.NewsService.Models;

public class ServiceCommandRepository(KeywordsManagementCommandContext context) :
    CommandRepository<KeywordsManagementCommandContext, NewsService>(context), INewsServiceCommandRepository
{ }