﻿using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class TokenizeController : ITokenizeController
    {
        private IFileNamesExtractor _fileNamesExtractor;
        private ITokenizer _tokenizer;

        public TokenizeController(IFileNamesExtractor fileNamesExtractor, ITokenizer tokenizer)
        {
            _fileNamesExtractor = fileNamesExtractor;
            _tokenizer = tokenizer;
        }

        public List<IWordOccurence> TokenizeFilesTerms(string relatedPath)
        {
            string[] filesRelatedPaths =
                _fileNamesExtractor.GetFilesRelatedPaths(relatedPath);
            var tokens = _tokenizer.TokenizeFiles(filesRelatedPaths);
            return tokens;
        }
    }
}