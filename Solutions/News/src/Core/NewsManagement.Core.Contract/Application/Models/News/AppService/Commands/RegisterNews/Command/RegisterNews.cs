namespace NewsManagement.Core.News.Contracts;

using Cloud.Web.Core.Contract;

public class RegisterNews : Command<long>
{
    public string Title { get; set; } = Empty;
    public string Description { get; set; } = Empty;
    public string Body { get; set; } = Empty;

    public List<string> KeywordsCodes { get; set; } = [];
}