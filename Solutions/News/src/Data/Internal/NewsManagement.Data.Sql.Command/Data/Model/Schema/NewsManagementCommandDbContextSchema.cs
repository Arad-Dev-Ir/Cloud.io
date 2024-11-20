namespace NewsManagement.Data.Sql.Commands;

public static class NewsManagementCommandDbContextSchema
{
    public const string DefaultSchema = "dbo";

    public static class NewsSchema
    {
        public const string TableName = "News";
    }

    public static class NewsKeywordsSchema
    {
        public const string TableName = "NewsKeywords";
    }
}