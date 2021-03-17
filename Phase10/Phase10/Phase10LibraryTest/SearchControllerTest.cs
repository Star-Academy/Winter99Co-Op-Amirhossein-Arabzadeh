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
            var input = "street +stories -last";
            var docs = new List<Doc>
            {
                new Doc("2", "street stories"),
                new Doc("1", "street"),
            };
            var mockSearchResponse = new Mock<ISearchResponse<Doc>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(docs);
            var mockingElasticClient = new Mock<IElasticClient>();
            mockingElasticClient.Setup(x => x
                    .Search(It.IsAny<Func<SearchDescriptor<Doc>, ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);
            var queryCreator = new QueryCreator();
            var searchController = new SearchController(mockingElasticClient.Object, queryCreator);
            var expectedDocs = new List<string>
            {
                "2",
                "1"
            };
            Assert.Equal(expectedDocs, searchController.SearchDocs(input));
        }
    }
}