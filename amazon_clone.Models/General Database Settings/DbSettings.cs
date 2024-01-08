namespace amazon_clone.Models.General_Database_Settings
{
    public class DbSettings
    {
        public int ID { get; set; }
        public DbInsertionHelper InsertionHelper { get; set; } = null!;
    }
}
