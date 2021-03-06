using System.Collections.Generic;


namespace InvertedIndexLibrary
{
    public class HashTableCreator : IHashTableCreator
    {
        private readonly ITokenizeController _tokenizeController;

        public HashTableCreator(ITokenizeController tokenizeController)
        {
            _tokenizeController = tokenizeController;
        }

        public Dictionary<string, List<string>> CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath)
        {
            var tokens = _tokenizeController.TokenizeFilesTerms(relatedPath);
            var tableOfWordsAsKeyAndContainingDocsAsValue =
                IterateTokensToMergeTheIdenticalTokens(tokens);

            return tableOfWordsAsKeyAndContainingDocsAsValue;
        }

        private Dictionary<string, List<string>> IterateTokensToMergeTheIdenticalTokens(IEnumerable<WordOccurrence> tokens)
        {
            var tableOfWordsAsKeyAndContainingDocsAsValue = new Dictionary<string, List<string>>();
            foreach (var wordOccurrence in tokens)
            {
                if (tableOfWordsAsKeyAndContainingDocsAsValue.ContainsKey(wordOccurrence.Term))
                {
                    var termDocsList = tableOfWordsAsKeyAndContainingDocsAsValue[wordOccurrence.Term];
                    if (!termDocsList.Contains(wordOccurrence.Doc))
                    {
                        termDocsList.Add(wordOccurrence.Doc);
                    }

                }
                else
                {
                    tableOfWordsAsKeyAndContainingDocsAsValue[wordOccurrence.Term] = new List<string> { wordOccurrence.Doc };
                }
            }

            return tableOfWordsAsKeyAndContainingDocsAsValue;
        }
    }
}