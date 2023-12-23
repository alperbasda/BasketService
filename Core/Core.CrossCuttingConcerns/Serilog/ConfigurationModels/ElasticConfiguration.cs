

namespace Core.CrossCuttingConcerns.Serilog.ConfigurationModels
{
    public class ElasticConfiguration
    {
        public string AppName { get; set; }

        public string Uri { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
