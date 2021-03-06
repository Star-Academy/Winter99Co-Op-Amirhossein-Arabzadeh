using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Xunit;

namespace InvertedIndexTest
{
    
    public class ListOperatorTest : TableProvider
    {

        private static readonly SampleDataProvider SampleDataProvider = SampleDataProvider.GetInstance();
        private static ListOperator _listOperator;
        private readonly Mock<IListCalculator> _listCalculator;
        private readonly InvertedIndexContext _invertedIndexContext;


        private void Seed(InvertedIndexContext invertedIndexContext)
        {
            var doc1 = new Doc(1.ToString());
            var doc2 = new Doc(2.ToString());
            var doc3 = new Doc(3.ToString());
            var searchItems = new List<SearchItem>
            {
                new SearchItem
                {
                    Term = "Ali",
                    Docs = new List<Doc>
                    {
                        doc1,
                        doc2,
                        doc3,
                    }
                },
                new SearchItem
                {
                    Term = "Hasan",
                    Docs = new List<Doc>
                    {
                        doc1
                    },
                },
                new SearchItem
                {
                    Term = "Hossein",
                    Docs = new List<Doc>
                    {
                        doc2,
                        doc3
                    },
                },
                new SearchItem
                {
                    Term = "Reza",
                    Docs = new List<Doc>
                    {
                        doc2,
                    },
                },
            };
            invertedIndexContext.SearchingItems.AddRange(searchItems);
            invertedIndexContext.SaveChanges();
        }
        public ListOperatorTest()
        {
            var option = new DbContextOptionsBuilder<InvertedIndexContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _invertedIndexContext = new InvertedIndexContext(option);
            _invertedIndexContext.Database.EnsureCreated();
            Seed(_invertedIndexContext);

            _listCalculator = new Mock<IListCalculator>();
            _listOperator = new ListOperator(_listCalculator.Object, _invertedIndexContext);
        }

        [Fact]
        public void 
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnInitializedResultSet_WhenParametersAreValid()
        {
            const string unsignedWord = "Ali";
            var expectedReturningList = new List<string>
            {
                "1",
                "2",
                "3"
            };
            Assert.Equal(expectedReturningList, _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(
                unsignedWord));
        }
        public static IEnumerable<object[]> InitializeResultSetByFirstUnsignedInputWordDocsArguments = new List<object[]>
        {
            new object[] {It.IsAny<string>(), new Dictionary<string, List<string>>()},
            new object[] {"", SampleDataProvider.Table},
            new object[] {" ", SampleDataProvider.Table},
            new object[] {"   ", SampleDataProvider.Table},
            new object[] {null, SampleDataProvider.Table},
        };

        [Theory]
        [MemberData(nameof(InitializeResultSetByFirstUnsignedInputWordDocsArguments))]
        public void
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnArgumentException_WhenArgumentsAreInvalid(string 
                unSignedWord, Dictionary<string, List<string>> table)
        {
            Action action = () => _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(unSignedWord);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void
            IntersectUnsignedWordsContainingDocs_ShouldReturnIntersectedSetOfUnsignedWordsContainingDocs_WhenParametersAreValid()
        {
            var unsignedWords = new List<string>
            {
                "Ali",
                "Hasan"
            };
            var inputResultList = new List<string>
            {
                "1",
                "2",
                "3"
            };
            var expectedReturn = new List<string>
            {
                "1"
            };
            Assert.Equal(expectedReturn, _listOperator.GetIntersectedUnsignedWordsContainingDocs(unsignedWords,
                inputResultList));
        }
        public static IEnumerable<object[]> GetIntersectedUnsignedWordsContainingDocsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), SampleDataProvider.Table},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), null},
            new object[] {It.IsAny<List<string>>(),null, It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {It.IsAny<List<string>>(),new List<string>(), It.IsAny<Dictionary<string, List<string>>>()},
        };

        [Theory]
        [MemberData(nameof(GetIntersectedUnsignedWordsContainingDocsArguments))]
        public void
            GetIntersectedUnsignedWordsContainingDocs_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                unSignedWord, List<string> inputResultList, Dictionary<string, List<string>> table)
        {
            Action action = () => _listOperator.GetIntersectedUnsignedWordsContainingDocs(unSignedWord,inputResultList);
            Assert.Throws<ArgumentException>(action);
        }
        
        
        [Fact]
        public void
            GetRemovedDocsWithoutPlusWords_ShouldReturnIntersectedSetOfUnsignedWordsContainingDocs_WhenParametersAreValid()
        {
            ISet<string> plusSignedWordsDocs = new HashSet<string>
            {
                "1",
                "2",
                "3",
            };
            _listCalculator.Setup(x => x.GetDocsOfWordsList(
                It.IsAny<List<string>>())).Returns(plusSignedWordsDocs);
            List<string> plusSignedWords = new List<string>
            {
                "ali",
                "reza",
            };
            List<string> inputResultList = new List<string>
            {
                "1",
                "2",
                "3",
                "10",
                "35",
            };
            List<string> expectedReturn = new List<string>
            {
                "1",
                "2",
                "3",
            };
            Assert.Equal(expectedReturn, _listOperator.GetDocsWithoutPlusWords(plusSignedWords,
                inputResultList));
        }
        public static IEnumerable<object[]> GetRemovedDocsWithoutPlusWordsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), SampleDataProvider.Table},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), null},
            new object[] {It.IsAny<List<string>>(),null, It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {It.IsAny<List<string>>(),new List<string>(), It.IsAny<Dictionary<string, List<string>>>()},
        };
        
        [Theory]
        [MemberData(nameof(GetRemovedDocsWithoutPlusWordsArguments))]
        public void
            GetRemovedDocsWithoutPlusWords_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                plusSignedWords, List<string> inputResultList, Dictionary<string, List<string>> table)
        {
            Action action = () => _listOperator.GetDocsWithoutPlusWords(plusSignedWords,inputResultList);
            Assert.Throws<ArgumentException>(action);
        }
        [Fact]
        public void
            GetRemovedDocsContainingMinusSignedWords_ShouldReturnIntersectedSetOfUnsignedWordsContainingDocs_WhenParametersAreValid()
        {
            ISet<string> plusSignedWordsDocs = new HashSet<string>
            {
                "1",
                "2",
                "3",
            };
            _listCalculator.Setup(x => x.GetDocsOfWordsList(
                It.IsAny<List<string>>())).Returns(plusSignedWordsDocs);
            List<string> minusSignedWords = new List<string>
            {
                "ali",
                "reza",
            };
            List<string> inputResultList = new List<string>
            {
                "1",
                "2",
                "3",
                "10",
                "35",
            };
            List<string> expectedReturn = new List<string>
            {
                "10",
                "35",
            };
            Assert.Equal(expectedReturn, _listOperator.GetDocsExcludingMinusSignedWords(minusSignedWords,
                inputResultList));
        }
        public static IEnumerable<object[]> GetRemovedDocsContainingMinusSignedWordsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), SampleDataProvider.Table},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), null},
            new object[] {It.IsAny<List<string>>(),null, It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {It.IsAny<List<string>>(),new List<string>(), It.IsAny<Dictionary<string, List<string>>>()},
        };
        
        [Theory]
        [MemberData(nameof(GetRemovedDocsContainingMinusSignedWordsArguments))]
        public void
            GetRemovedDocsContainingMinusSignedWords_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                minusSignedWords, List<string> inputResultList, Dictionary<string, List<string>> table)
        {
            Action action = () => _listOperator.GetDocsExcludingMinusSignedWords(minusSignedWords,inputResultList);
            Assert.Throws<ArgumentException>(action);
        }


        
    }
}