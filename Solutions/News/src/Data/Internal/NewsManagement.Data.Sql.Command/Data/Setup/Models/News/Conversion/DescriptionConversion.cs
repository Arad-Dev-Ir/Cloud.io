namespace NewsManagement.Data.Sql.News.Commands;

using Cloud.Web.Data.Sql.Command;
using NewsDescription = Core.News.Models.NewsDescription;

internal class DescriptionConversion : Conversion<NewsDescription, string>
{
    public DescriptionConversion() : base(e => e.Value, e => NewsDescription.Instance(e))
    { }
}