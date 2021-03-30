using Microsoft.Extensions.Configuration;
using Phase10Library;

namespace ImportRun
{
    class Program
    {
        private const string AppsettingsJsonFile = "appsettings.json";

        static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var settings = configuration.Get<Settings>();
            
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.host);
            
            var elasticResponseValidator = new ElasticResponseValidator();
            var indexDefiner = new IndexDefiner(elasticClient, elasticResponseValidator, settings);
            indexDefiner.CreateIndex(settings.Indexes.DocsIndex);
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppsettingsJsonFile, false, true)
                .Build();
            return configuration;
        }
    }
}