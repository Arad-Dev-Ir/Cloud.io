namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloudio.Web.Data.Sql.Command;
using Core.NewsService.Contracts;
using KeywordsManagement.Core.NewsService.Models;
using KeywordsManagement.Data.Sql.Commands;

public class ServiceCommandRepository(KeywordsManagementCommandDbContext context)
    : CommandRepository<KeywordsManagementCommandDbContext, NewsService>(context), INewsServiceCommandRepository
{ }