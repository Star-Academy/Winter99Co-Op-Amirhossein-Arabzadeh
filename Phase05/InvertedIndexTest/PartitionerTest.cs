using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Moq;
using Xunit;

namespace InvertedIndexTest
{
    public class PartitionerTest
    {
        private readonly IPartitioner _partitioner;
        public PartitionerTest()
        { 
            _partitioner = new Partitioner();
        }

        [Fact]
        public void GetWantedSignedWords_ShouldReturnSignedWords_WhenParametersAreValid()
        {
            
            const string sampleSearchingTerm = "ali reza +mohammad +ghasem -fatemeh -zahra";
            var expectedReturningList = new List<string>
            {
                "mohammad",
                "ghasem",
                
            };
            Assert.Equal(expectedReturningList, _partitioner.GetWantedSignedWords(sampleSearchingTerm, "+"));

        }
        public static IEnumerable<object[]> GetWantedSignedWordsInvalidArguments = new List<object[]>
        {
            new object[] {It.IsAny<string>(), null},
            new object[] {It.IsAny<string>(), "   " },
            new object[] {It.IsAny<string>(), " " },
            new object[] {" " , It.IsAny<string>()},
            new object[] {"   " , It.IsAny<string>()},
        };
        [Theory]
        [MemberData(nameof(GetWantedSignedWordsInvalidArguments))]
        public void GetWantedSignedWords_ShouldThrowArgumentException_WhenParametersAreInvalid(string searchingTerm,
            string sign)
        {
            Action action = () =>
                _partitioner.GetWantedSignedWords(searchingTerm, sign);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void GetUnSignedWords_ShouldReturnUnsignedWords_WhenParametersAreValid()
        {
            const string sampleSearchingTerm = "ali reza +mohammad +ghasem -fatemeh -zahra";
            var expectedReturningList = new List<string>
            {
                "ali",
                "reza",
                
            };
            Assert.Equal(expectedReturningList, _partitioner.GetUnSignedWords(sampleSearchingTerm));

        }
        public static IEnumerable<object[]> GetUnsignedWordsInvalidArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {" "},
            new object[] {"   "},
        };
        [Theory]
        [MemberData(nameof(GetUnsignedWordsInvalidArguments))]
        public void GetUnsignedWords_ShouldThrowArgumentException_WhenParametersAreInvalid(string searchingTerm)
        {
            Action action = () =>
                _partitioner.GetUnSignedWords(searchingTerm);
            Assert.Throws<ArgumentException>(action);
        }
    }
}