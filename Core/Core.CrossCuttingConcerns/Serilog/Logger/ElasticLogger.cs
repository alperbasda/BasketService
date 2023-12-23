using Serilog.Debugging;
using Serilog.Filters;
using Serilog;
using Microsoft.Extensions.Configuration;
using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Sinks.Elasticsearch;

namespace Core.CrossCuttingConcerns.Serilog.Logger
{
    public class ElasticLogger : LoggerServiceBase
    {
        public ElasticLogger(IConfiguration configuration)
        {
            ElasticConfiguration options =
            configuration.GetSection("SeriLogConfigurations:ElasticConfiguration").Get<ElasticConfiguration>()
            ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            // Enable the selflog output
            SelfLog.Enable(Console.Error);
            base.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
                .Filter.ByExcluding(Matching.FromSource("Microsoft"))
                .Filter.ByExcluding(Matching.FromSource("System"))
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .Enrich.WithProperty("Application", options.AppName)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(options.Uri))
            {
                ModifyConnectionSettings = x => x.BasicAuthentication(options.UserName, options.Password),
                AutoRegisterTemplate = true,
                OverwriteTemplate = true,
                DetectElasticsearchVersion = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                NumberOfReplicas = 1,
                NumberOfShards = 2,
                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
            }).CreateLogger();
            Log.Logger = base.Logger;

        }
    }
}
