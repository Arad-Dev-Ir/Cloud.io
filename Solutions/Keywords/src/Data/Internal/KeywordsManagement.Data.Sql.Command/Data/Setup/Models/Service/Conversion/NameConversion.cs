namespace KeywordsManagement.Data.Sql.NewsService.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsServiceName = Core.NewsService.Models.NewsServiceName;

public class NameConversion : Conversion<NewsServiceName, string>
{
    public NameConversion() : base(e => e.Value, e => NewsServiceName.Instance(e))
    { }
}