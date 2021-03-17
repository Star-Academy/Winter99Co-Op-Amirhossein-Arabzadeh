using System.Collections.Generic;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ImporterTest
    {
        [Fact]
        public void Import_ShouldBulkDocsWithoutAnyError_WhenParametersAreValid()
        {
            var docs = new List<Doc>
            {
                new Doc("properName", "proper content"),
                new Doc("properName2", "proper content2"),
            };
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, Indexes.DocsIndex);
        }
    }
}