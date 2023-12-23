
namespace Core.Persistence.Models
{
    public class DatabaseOptions
    {
        public string? EfConnectionString { get; set; }

        public string? MongoConnectionString { get; set; }

        public string? DatabaseName { get; set; }
    }
}
