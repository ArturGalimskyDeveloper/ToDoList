namespace TodoList
{
    public class Configuration
    {
        public const string CONNECTION_STRING = "Data Source=tasks.db;Mode=ReadWriteCreate";
        
        // your token here
        public static readonly string TOKEN = System.Environment.GetEnvironmentVariable("token_estbot");
    }
}