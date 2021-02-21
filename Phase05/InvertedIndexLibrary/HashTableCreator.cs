using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class HashTableCreator : IHashTableCreator
    {
        private ITokenizeController _tokenizeController;
        private IFileNamesExtractor _fileNamesExtractor;
        private ITokenizer _tokenizer;

        public HashTableCreator(ITokenizeController tokenizeController, IFileNamesExtractor fileNamesExtractor, ITokenizer tokenizer)
        {
            _tokenizeController = tokenizeController;
            _fileNamesExtractor = fileNamesExtractor;
            _tokenizer = tokenizer;
        }

        public IDictionary<string, List<string>> createHashTableOfWordsAsKeyAndContainingDocsAsValue( string relatedPath)
        {
            List<IWordOccurence> tokens = _tokenizeController.TokenizeFilesTerms(relatedPath);
            IDictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue =
                new Dictionary<string, List<string>>();
            foreach (var wordOccurence in tokens)
            {
                if (tableOfWordsAsKeyAndContainingDocsAsValue.ContainsKey(wordOccurence.Term))
                {
                    tableOfWordsAsKeyAndContainingDocsAsValue[wordOccurence.Term].Add(wordOccurence.Doc);
                }
                else
                {
                    tableOfWordsAsKeyAndContainingDocsAsValue[wordOccurence.Term] = new List<string> {wordOccurence.Doc};
                }

            }

            return tableOfWordsAsKeyAndContainingDocsAsValue;
        }
    }
}