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
        private string[] _fileNames = null;
        private ITokenizer _tokenizer;
        public TokenizerTest()
        { 
            _fileNames = new string[] {"58043", "58044"}; 
            _tokenizer = new Tokenizer();
        }

        [Theory]
        [InlineData("../../../../Resources/SmallEnglishData")]
        public void TokenizeFiles_ShouldReturnListOfTokensOfWordsInDirectory_WhenDirectoryPathIsValidAndNotEmpty(string relativePath)
        { 
            
            
            var filesRelativePaths = _fileNames.Select(s => relativePath+ "\\" + s );
            
            var _tokens = new List<IWordOccurence>
            {
                new WordOccurrence("ali", "58043"),
                new WordOccurrence("hasan", "58043"),
                new WordOccurrence("hossein", "58043"),
                new WordOccurrence("ali", "58044"),
                new WordOccurrence("reza", "58044"),
                new WordOccurrence("javad", "58044")
            };
            
            Assert.Equal(_tokens, _tokenizer.TokenizeFiles(filesRelativePaths));

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