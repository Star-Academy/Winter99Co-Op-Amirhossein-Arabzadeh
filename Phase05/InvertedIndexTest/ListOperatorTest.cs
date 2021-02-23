using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Moq;
using Xunit;

namespace InvertedIndexTest
{
    public class ListOperatorTest
    {
        private static SampleDataProvider _sampleDataProvider = SampleDataProvider.GetInstance();
        private static ListOperator _listOperator;
        private Mock<IListCalculator> _listCalculator; 

        public ListOperatorTest()
        {
            _listCalculator = new Mock<IListCalculator>();
            _listOperator = new ListOperator(_listCalculator.Object);
        }

        [Fact]
        public void 
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnInitializedResultSet_WhenParametersAreValid()
        {
            string unsignedWord = "ali";
            List<string> expectedReturningList = new List<string>
            {
                "1",
                "2",
                "3"
            };
            Assert.Equal(expectedReturningList, _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(
                unsignedWord, _sampleDataProvider.Table));
        }
        public static IEnumerable<object[]> InitializeResultSetByFirstUnsignedInputWordDocsArguments = new List<object[]>
        {
            new object[] {It.IsAny<string>(), new Dictionary<string, List<string>>()},
            new object[] {"", _sampleDataProvider.Table},
            new object[] {" ", _sampleDataProvider.Table},
            new object[] {"   ", _sampleDataProvider.Table},
            new object[] {null, _sampleDataProvider.Table},
        };

        [Theory]
        [MemberData(nameof(InitializeResultSetByFirstUnsignedInputWordDocsArguments))]
        public void
            InitializeResultSetByFirstUnsignedInputWordDocs_ShouldReturnArgumentException_WhenArgumentsAreInvalid(string 
                unSignedWord, Dictionary<string, List<string>> table)
        {
            Action action = () => _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(unSignedWord, table);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void
            IntersectUnsignedWordsContainingDocs_ShouldReturnIntersectedSetOfUnsignedWordsContainingDocs_WhenParametersAreValid()
        {
            List<string> unsignedWords = new List<string>
            {
                "ali",
                "reza",
                "javad"
            };
            List<string> inputResultList = new List<string>
            {
                "1",
                "2",
                "3"
            };
            List<string> expectedReturn = new List<string>
            {
                "1"
            };
            Assert.Equal(expectedReturn, _listOperator.GetIntersectedUnsignedWordsContainingDocs(unsignedWords,
                inputResultList, _sampleDataProvider.Table));
        }
        public static IEnumerable<object[]> GetIntersectedUnsignedWordsContainingDocsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), _sampleDataProvider.Table},
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
            Action action = () => _listOperator.GetIntersectedUnsignedWordsContainingDocs(unSignedWord,inputResultList, table);
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
                It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>())).Returns(plusSignedWordsDocs);
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
                inputResultList, _sampleDataProvider.Table));
        }
        public static IEnumerable<object[]> GetRemovedDocsWithoutPlusWordsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), _sampleDataProvider.Table},
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
            Action action = () => _listOperator.GetDocsWithoutPlusWords(plusSignedWords,inputResultList, table);
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
                It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>())).Returns(plusSignedWordsDocs);
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
            Assert.Equal(expectedReturn, _listOperator.GetRemovedDocsExcludingMinusSignedWords(minusSignedWords,
                inputResultList, _sampleDataProvider.Table));
        }
        public static IEnumerable<object[]> GetRemovedDocsContainingMinusSignedWordsArguments = new List<object[]>
        {
            new object[] {It.IsAny<List<string>>(),It.IsAny<List<string>>(), new Dictionary<string, List<string>>()},
            new object[] {null,It.IsAny<List<string>>(), It.IsAny<Dictionary<string, List<string>>>()},
            new object[] {new List<string>(),It.IsAny<List<string>>(), _sampleDataProvider.Table},
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
            Action action = () => _listOperator.GetRemovedDocsExcludingMinusSignedWords(minusSignedWords,inputResultList, table);
            Assert.Throws<ArgumentException>(action);
        }
        
    }
}