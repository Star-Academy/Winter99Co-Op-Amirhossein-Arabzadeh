using System;
using Microsoft.Extensions.Configuration;
using Phase10Library;

namespace ImportRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.HttpLocalhost);
            var elasticResponseValidator = new ElasticResponseValidator();
            var indexDefiner = new IndexDefiner(elasticClient, elasticResponseValidator, settings);
            indexDefiner.CreateIndex(settings.Indexes.DocsIndex);
        }
    }
}