using System.Collections.Generic;
using ImportRun;
using Microsoft.Extensions.Configuration;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ImporterTest
    {
        [Fact]
        public void Import_ShouldBulkDocsWithoutAnyError_WhenParametersAreValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            const string properName2 = "properName2";
            const string properName1 = "properName";
            var docs = new List<Doc>
            {
                new Doc(properName1, "proper content"),
                new Doc(properName2, "proper content2"),
            };
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.HttpLocalhost);
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, settings.Indexes.DocsIndex);
        }

         
    }
}