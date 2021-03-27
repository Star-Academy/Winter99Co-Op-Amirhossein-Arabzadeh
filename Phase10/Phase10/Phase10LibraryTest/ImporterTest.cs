using System.Collections.Generic;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ImporterTest
    {
        [Fact]
        public void Import_ShouldBulkDocsWithoutAnyError_WhenParametersAreValid()
        {
            const string properName2 = "properName2";
            const string properName1 = "properName";
            var docs = new List<Doc>
            {
                new Doc(properName1, "proper content"),
                new Doc(properName2, "proper content2"),
            };
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, Indexes.DocsIndex);
        }

         
    }
}