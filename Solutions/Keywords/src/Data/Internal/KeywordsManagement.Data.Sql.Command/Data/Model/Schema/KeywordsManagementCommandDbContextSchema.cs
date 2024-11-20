namespace KeywordsManagement.Data.Sql.Commands;

public static class KeywordsManagementCommandDbContextSchema
{
    public const string DefaultSchema = "dbo";

    public static class KeywordSchema
    {
        public const string TableName = "Keywords";
    }

    public static class NewsServicesSchema
    {
        public const string TableName = "NewsServices";
    }
}