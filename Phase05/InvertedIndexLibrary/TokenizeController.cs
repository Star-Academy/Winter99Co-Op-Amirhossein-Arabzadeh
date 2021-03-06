﻿using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class TokenizeController : ITokenizeController
    {
        private readonly IFileNamesExtractor _fileNamesExtractor;
        private readonly ITokenizer _tokenizer;

        public TokenizeController(IFileNamesExtractor fileNamesExtractor, ITokenizer tokenizer)
        {
            _fileNamesExtractor = fileNamesExtractor;
            _tokenizer = tokenizer;
        }

        public List<WordOccurrence> TokenizeFilesTerms(string relatedPath)
        {
            var filesRelatedPaths =
                _fileNamesExtractor.GetFilesRelatedPaths(relatedPath);
            var tokens = _tokenizer.TokenizeFiles(filesRelatedPaths);
            return tokens;
        }
    }
}