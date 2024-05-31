namespace KeywordsManagement.Data.Sql.Keyword.Commands;

using Cloud.Web.Data.Sql.Command;
using Core.Keyword.Contracts;
using Sql.Commands;
using Keyword = Core.Keyword.Models.Keyword;

public class KeywordCommandRepository : CommandRepository<BasicInformationCommandContext, Keyword>, IKeywordCommandRepository
{
    public KeywordCommandRepository(BasicInformationCommandContext context) : base(context)
    { }
}