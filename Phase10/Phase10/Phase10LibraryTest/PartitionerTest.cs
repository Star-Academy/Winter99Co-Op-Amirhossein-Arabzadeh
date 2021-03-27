using System;
using System.Collections.Generic;
using Moq;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
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
            const string word2 = "ghasem";
            const string word1 = "mohammad";
            const string sampleSearchingTerm = "ali reza +mohammad +ghasem -fatemeh -zahra";
            var expectedReturningList = new List<string>
            {
                word1,
                word2,
                
            };
            Assert.Equal(expectedReturningList, _partitioner.GetSignedWords(sampleSearchingTerm, "+"));

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
            Action action = () => _partitioner.GetSignedWords(searchingTerm, sign);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void GetUnSignedWords_ShouldReturnUnsignedWords_WhenParametersAreValid()
        {
            const string word1 = "ali";
            const string word2 = "reza";
            const string sampleSearchingTerm = "ali reza +mohammad +ghasem -fatemeh -zahra";
            var expectedReturningList = new List<string>
            {
                word1,
                word2,
                
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
            Action action = () => _partitioner.GetUnSignedWords(searchingTerm);
            Assert.Throws<ArgumentException>(action);
        }
    }
}