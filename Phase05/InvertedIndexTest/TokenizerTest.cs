using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InvertedIndexLibrary;
using Xunit;

namespace InvertedIndexTest
{
    public class TokenizerTest
    {
        private readonly string[] _fileNames;
        private readonly ITokenizer _tokenizer;
        public TokenizerTest()
        { 
            _fileNames = new[] {"58043", "58044"}; 
            _tokenizer = new Tokenizer();
        }

        
        [Theory]
        [InlineData("../../../../Resources/SmallEnglishData")]
        public void TokenizeFiles_ShouldReturnListOfTokensOfWordsInDirectory_WhenDirectoryPathIsValidAndNotEmpty(string relativePath)
        { 
            
            
            var filesRelativePaths = _fileNames.Select(s => relativePath+ "\\" + s );
            
            var tokens = new List<WordOccurrence>
            {
                new WordOccurrence("ali", "58043"),
                new WordOccurrence("hasan", "58043"),
                new WordOccurrence("hossein", "58043"),
                new WordOccurrence("ali", "58044"),
                new WordOccurrence("reza", "58044"),
                new WordOccurrence("javad", "58044")
            };
            
            Assert.Equal(tokens, _tokenizer.TokenizeFiles(filesRelativePaths));
        
        }
        [Theory]
        [InlineData("../../../../Resources/SmallEnglishDatas")]
        public void TokenizeFiles_ShouldThrowDirectoryNotFoundException_WhenDirectoryPathIsInvalidButNotEmpty(string relativePath)
        { 
            
            
            var filesRelativePaths = _fileNames.Select(s => relativePath+ "\\" + s );


            Action action = () => _tokenizer.TokenizeFiles(filesRelativePaths);
            Assert.Throws<FileNotFoundException>(action);

        }
    }
}