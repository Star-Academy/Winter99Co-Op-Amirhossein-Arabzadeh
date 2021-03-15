using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Moq;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ListCalculatorTest
    {
        private static IListCalculator _listCalculator;
        private static readonly SampleDataProvider SampleDataProvider = SampleDataProvider.GetInstance();
        private readonly ElasticClientFactory _elasticClientFactory;


        public ListCalculatorTest()
        {
            var docs = new HashSet<Doc>
            {
                new Doc("1", "It.IsAny<string>()"),
                new Doc("2", "It.IsAny<string>()"),
            };

            var mockSearchResponse = new Mock<ISearchResponse<Doc>>();
            mockSearchResponse.Setup(x => x.Documents).Returns(docs);
            
            var mockElasticClient = new Mock<IElasticClient>();
            mockElasticClient
                .Setup(x => x.Search<Doc>(It.IsAny<Func<SearchDescriptor<Doc>
                    , ISearchRequest>>()))
                .Returns(mockSearchResponse.Object);

            _listCalculator = new ListCalculator(mockElasticClient.Object);
            
        }

        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            
            Assert.Equal(new HashSet<string>{"1", "2"}, _listCalculator.GetDocsOfWordsList(new List<string>
            {
                "hasan",
                "reza",
            }));
            
            
        }


        [Theory]
        [MemberData(nameof(InvalidListAndDictionaryArguments))]
        public void
            CreateSetOfDifferentPartitions_ShouldThrowArgumentException_WhenParametersAreInvalid(
                List<string> partition)
        {
            void Action() => _listCalculator.GetDocsOfWordsList(partition);
            Assert.Throws<ArgumentException>(Action);
        }

        [Fact]
        public void MinusElementsOfSetFromList_ShouldReturnInputListWithoutElementOfInputSet_WhenParametersAreValid()
        {
            var sampleInputList = (from number in  Enumerable.Range(1, 60)
                    select number.ToString()).ToList();
            ISet<string> sampleInputSet = (from number in  Enumerable.Range(1, 60)
                select number.ToString()).ToHashSet();
            sampleInputSet.Remove("1");
            sampleInputSet.Remove("3");
            sampleInputSet.Remove("8");
            sampleInputSet.Remove("58");
            var expectedList = new List<string>
            {
                "1",
                "3",
                "8",
                "58",
            };
            Assert.Equal(expectedList, _listCalculator.MinusElementsOfSetFromList(sampleInputSet, sampleInputList));
        }

        [Theory]
        [MemberData(nameof(InvalidSetAndListArguments))]
        public void
            MinusElementsOfSetFromList_ShouldThrowArgumentException_WhenParametersAreInvalid(
                ISet<string> set, List<string> list)
        {
            void Action() => _listCalculator.MinusElementsOfSetFromList(set, list);
            Assert.Throws<ArgumentException>(Action);
        }
        [Fact]
        public void AndListWithSet_ShouldReturnInputListWithoutElementOfInputSet_WhenParametersAreValid()
        {
            var sampleInputList = (from number in  Enumerable.Range(1, 60)
                select number.ToString()).ToList();
            ISet<string> sampleInputSet = (from number in  Enumerable.Range(1, 5)
                select number.ToString()).ToHashSet();
            
            var expectedList = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
            };
            Assert.Equal(expectedList, _listCalculator.AndListWithSet(sampleInputSet, sampleInputList));
        }

        [Theory]
        [MemberData(nameof(InvalidSetAndListArguments))]
        public void
            AndListWithSet_ShouldThrowArgumentException_WhenParametersAreInvalid(
                ISet<string> set, List<string> list)
        {
            void Action() => _listCalculator.AndListWithSet(set, list);
            Assert.Throws<ArgumentException>(Action);
        }


        public static IEnumerable<object[]> InvalidSetAndListArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new HashSet<string>(), null},
            new object[] {new HashSet<string>(), SampleDataProvider.Partition},
            new object[] {SampleDataProvider.ExpectedSet, null},
            new object[] {SampleDataProvider.ExpectedSet, new List<string>()},
        };


        public static IEnumerable<object[]> InvalidListAndDictionaryArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {new List<string>()},
            new object[] {new List<string>()},
        };
    }
}