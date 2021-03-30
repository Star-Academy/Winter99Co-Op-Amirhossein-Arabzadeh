using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class SearchControllerTest
    {
        private const string AppsettingsJson = "test-appsettings.json";

        [Fact]
        public void SearchDocs_ShouldReturnResultWithoutAnyException_WhenParameterIsValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppsettingsJson, false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            const string name2 = "1";
            const string content2 = "street";
            const string content1 = "street stories";
            const string name1 = "2";
            const string input = "street +stories -last";
            var docs = new List<Doc>
            {
                new(name1, content1),
                new(name2, content2),
            };
            var mockSearchResponse = new Mock<ISearchResponse<Doc>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(docs);
            var mockingElasticClient = new Mock<IElasticClient>();
            mockingElasticClient.Setup(x => x
                    .Search(It.IsAny<Func<SearchDescriptor<Doc>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);
            var elasticResponseValidator = new ElasticResponseValidator();
            var searchController = new SearchController(mockingElasticClient.Object, elasticResponseValidator, settings);
            const string doc1Name = "1";
            const string doc2Name = "2";
            var expectedDocs = new List<string>
            {
                doc2Name,
                doc1Name
            };
            Assert.Equal(expectedDocs, searchController.SearchDocs(input));
        }
    }
}