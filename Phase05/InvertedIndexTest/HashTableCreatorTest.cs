﻿using System.Collections.Generic;
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

            var expectedTable = new Dictionary<string, List<string>>
            {
                ["ali"] = new List<string> {"1", "2", "3"},
                ["reza"] = new List<string> {"1"},
                ["javad"] = new List<string> {"1", "10", "35"},
                ["hossein"] = new List<string> {"35"}
            };


            var fileNamesExtractor = new Mock<IFileNamesExtractor>();
            fileNamesExtractor.Setup(
                x => x.GetFilesRelatedPaths(It.IsAny<string>())).Returns(
                It.IsAny<string[]>());
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.Setup(x => x.TokenizeFiles(It.IsAny<IEnumerable<string>>())).Returns(It.IsAny<List<WordOccurrence>>());
            var tokenizeController = new Mock<ITokenizeController>();
            tokenizeController.Setup(x => x.TokenizeFilesTerms(It.IsAny<string>())).Returns(new List<WordOccurrence>
            {
                new WordOccurrence("ali", "1"),
                new WordOccurrence("ali", "1"),
                new WordOccurrence("ali", "2"),
                new WordOccurrence("ali", "3"),
                new WordOccurrence("reza", "1"),
                new WordOccurrence("javad", "1"),
                new WordOccurrence("javad", "1"),
                new WordOccurrence("javad", "1"),
                new WordOccurrence("javad", "10"),
                new WordOccurrence("javad", "35"),
                new WordOccurrence("hossein", "35")
            });
            IHashTableCreator hashTableCreator = new HashTableCreator(tokenizeController.Object);
            var actualTable = hashTableCreator.CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(Arg.Any<string>());
            Assert.Equal(expectedTable, actualTable);
        }
    }
}