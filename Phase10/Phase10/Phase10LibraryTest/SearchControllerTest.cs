using System;
using System.Collections.Generic;
using Moq;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class SearchControllerTest
    {
        [Fact]
        public void SearchDocs_ShouldReturnResultWithoutAnyException_WhenParameterIsValid()
        {
            
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
            var searchController = new SearchController(mockingElasticClient.Object, elasticResponseValidator);
            var expectedDocs = new List<string>
            {
                "2",
                "1"
            };
            Assert.Equal(expectedDocs, searchController.SearchDocs(input));
        }

    }
}