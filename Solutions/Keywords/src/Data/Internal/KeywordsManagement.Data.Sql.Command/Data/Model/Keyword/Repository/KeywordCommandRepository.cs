namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloudio.Web.Data.Sql.Command;
using KeywordsManagement.Core.Keyword.Contracts;
using KeywordsManagement.Data.Sql.Commands;
using KeywordsManagement.Core.Keyword.Models;

public sealed class KeywordCommandRepository(KeywordsManagementCommandDbContext context)
    : CommandRepository<KeywordsManagementCommandDbContext, Keyword>(context), IKeywordCommandRepository
{ }