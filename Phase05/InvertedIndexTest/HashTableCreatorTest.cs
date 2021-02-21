using System.Collections;
using System.Collections.Generic;
using InvertedIndexLibrary;
using NSubstitute;
using Xunit;
using Moq;


namespace InvertedIndexTest
{
    public class HashTableCreatorTest
    { 
        

        [Fact]
        public void CreateHashTable_ShouldReturnValidTableOfWordsAsKeyAndDocsAsValue_WhenParameterIsValid()
        {

            Dictionary<string, List<string>> expectedTable = new Dictionary<string, List<string>>();
            expectedTable["ali"] = new List<string>
            {
                "1", "2", "3"
            };
            expectedTable["reza"] = new List<string>
            {
                "1"
            };
            expectedTable["javad"] = new List<string>
            {
                "1", "10", "35"
            };
            expectedTable["hossein"] = new List<string>
            {
                "35"
            };


            var fileNamesExtractor = Substitute.For<IFileNamesExtractor>();
            fileNamesExtractor.GetFilesRelatedPaths(Arg.Any<string>()).Returns(new string[2]);
            var tokenizer = Substitute.For<ITokenizer>();
            tokenizer.TokenizeFiles(Arg.Any<string[]>()).Returns(new List<IWordOccurence>());
            var tokenizeController = Substitute.For<ITokenizeController>();
            tokenizeController.TokenizeFilesTerms(Arg.Any<string>()).Returns(new List<IWordOccurence>
            {
                new WordOccurence("ali", "1"),
                new WordOccurence("ali", "2"),
                new WordOccurence("ali", "3"),
                new WordOccurence("reza", "1"),
                new WordOccurence("javad", "1"),
                new WordOccurence("javad", "10"),
                new WordOccurence("javad", "35"),
                new WordOccurence("hossein", "35")
            });
            IHashTableCreator hashTableCreator = new HashTableCreator(tokenizeController, fileNamesExtractor, tokenizer);
            Assert.Equal(expectedTable, hashTableCreator.createHashTableOfWordsAsKeyAndContainingDocsAsValue(Arg.Any<string>()));
        }
    }
}