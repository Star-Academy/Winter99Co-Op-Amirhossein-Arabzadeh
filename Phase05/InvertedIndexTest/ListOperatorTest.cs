using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Moq;
using Xunit;

namespace InvertedIndexTest
{
    
    public class ListOperatorTest : TableProvider, IDisposable
    {
        private static ListOperator _listOperator;
        private readonly Mock<IListCalculator> _listCalculator;
        private readonly InvertedIndexContext _invertedIndexContext;


        public ListOperatorTest()
        {
            var testDbContextFactory = new TestDbContextFactory();
            _invertedIndexContext = testDbContextFactory.Seed();
            
            _listCalculator = new Mock<IListCalculator>();
            _listOperator = new ListOperator(_listCalculator.Object, _invertedIndexContext);
        }

        [Fact]
        public void 
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnInitializedResultSet_WhenParametersAreValid()
        {
            const string unsignedWord = "ali";
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
            new object[] {It.IsAny<string>()},
            new object[] {""},
            new object[] {" "},
            new object[] {"   "},
            new object[] {null},
        };

        [Theory]
        [MemberData(nameof(InitializeResultSetByFirstUnsignedInputWordDocsArguments))]
        public void
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnArgumentException_WhenArgumentsAreInvalid(string 
                unSignedWord)
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
                "ali",
                "hasan"
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
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {null,It.IsAny<List<string>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),null},
            new object[] {It.IsAny<List<string>>(),new List<string>()},
        };

        [Theory]
        [MemberData(nameof(GetIntersectedUnsignedWordsContainingDocsArguments))]
        public void
            GetIntersectedUnsignedWordsContainingDocs_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                unSignedWord, List<string> inputResultList)
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
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {null,It.IsAny<List<string>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),null},
            new object[] {It.IsAny<List<string>>(),new List<string>()},
        };
        
        [Theory]
        [MemberData(nameof(GetRemovedDocsWithoutPlusWordsArguments))]
        public void
            GetRemovedDocsWithoutPlusWords_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                plusSignedWords, List<string> inputResultList)
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
            var minusSignedWords = new List<string>
            {
                "ali",
                "reza",
            };
            var inputResultList = new List<string>
            {
                "1",
                "2",
                "3",
                "10",
                "35",
            };
            var expectedReturn = new List<string>
            {
                "10",
                "35",
            };
            Assert.Equal(expectedReturn, _listOperator.GetDocsExcludingMinusSignedWords(minusSignedWords,
                inputResultList));
        }
        public static IEnumerable<object[]> GetRemovedDocsContainingMinusSignedWordsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {null,It.IsAny<List<string>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>()},
            new object[] {It.IsAny<List<string>>(),null},
            new object[] {It.IsAny<List<string>>(),new List<string>()},
        };
        
        [Theory]
        [MemberData(nameof(GetRemovedDocsContainingMinusSignedWordsArguments))]
        public void
            GetRemovedDocsContainingMinusSignedWords_ShouldReturnArgumentException_WhenArgumentsAreInvalid(List<string>
                minusSignedWords, List<string> inputResultList)
        {
            void Action() => _listOperator.GetDocsExcludingMinusSignedWords(minusSignedWords, inputResultList);
            Assert.Throws<ArgumentException>((Action) Action);
        }


        public void Dispose()
        {
            _invertedIndexContext.Database.EnsureDeleted();
            _invertedIndexContext.Dispose();
        }

    }
}