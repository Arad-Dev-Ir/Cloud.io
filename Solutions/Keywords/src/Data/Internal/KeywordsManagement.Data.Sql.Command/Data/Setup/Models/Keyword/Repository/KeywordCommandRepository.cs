namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Contracts;
using Sql.Commands;
using Keyword = Core.Keyword.Models.Keyword;

public sealed class KeywordCommandRepository(KeywordsManagementCommandContext context) :
    CommandRepository<KeywordsManagementCommandContext, Keyword>(context), IKeywordCommandRepository
{ }