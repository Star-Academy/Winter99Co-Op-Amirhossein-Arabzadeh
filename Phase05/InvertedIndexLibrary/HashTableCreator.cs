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

        private Dictionary<string, List<string>> IterateTokensToMergeTheIdenticalTokens(IEnumerable<IWordOccurence> tokens)
        {
            var tableOfWordsAsKeyAndContainingDocsAsValue =
                new Dictionary<string, List<string>>();
            foreach (var wordOccurence in tokens)
            {
                if (tableOfWordsAsKeyAndContainingDocsAsValue.ContainsKey(wordOccurence.Term))
                {
                    var termDocsList = tableOfWordsAsKeyAndContainingDocsAsValue[wordOccurence.Term];
                    if (!termDocsList.Contains(wordOccurence.Doc))
                    {
                        termDocsList.Add(wordOccurence.Doc);    
                    }
                    
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