using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ImporterTest
    {
        private const string AppsettingsJson = "test-appsettings.json";

        [Fact]
        public void Import_ShouldBulkDocsWithoutAnyError_WhenParametersAreValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppsettingsJson, false, true)
                .Build();

            var settings = configuration.Get<Settings>();
            
            var docs = CreateDocsList();
            
            var elasticClient = new Mock<IElasticClient>();
            var bulkDescriptor = new BulkDescriptor();
            foreach (var document in docs)
            {
                bulkDescriptor.Index<Doc>(x => x
                    .Index(settings.Indexes.DocsIndex)
                    .Document(document)
                );
            }
            elasticClient.Setup(m => m.Bulk(bulkDescriptor));
            
            var importer = new Importer<Doc>(elasticClient.Object);
            importer.Import(docs, settings.Indexes.DocsIndex);

            elasticClient.Verify(m => m.Bulk(It.IsAny<BulkDescriptor>()), Times.Once);
        }

        private IEnumerable<Doc> CreateDocsList()
        {
            const string properName2 = "properName2";
            const string properName1 = "properName";
            const string properContent1 = "proper content";
            const string properContent2 = "proper content2";
            var docs = new List<Doc>
            {
                new Doc(properName1, properContent1),
                new Doc(properName2, properContent2),
            };
            return docs;
        }
    }
}