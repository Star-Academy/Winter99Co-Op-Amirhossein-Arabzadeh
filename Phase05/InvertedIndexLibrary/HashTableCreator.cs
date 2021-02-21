using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class HashTableCreator : IHashTableCreator
    {
        private ITokenizeController _tokenizeController;
        
        public HashTableCreator(ITokenizeController tokenizeController)
        {
            _tokenizeController = tokenizeController;
           
        }

        public IDictionary<string, List<string>> createHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath)
        {
            List<IWordOccurence> tokens = _tokenizeController.TokenizeFilesTerms(relatedPath);
            IDictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue =
                new Dictionary<string, List<string>>();
            
            IterateTokensToMergeTheIdenticalTokens(tokens, tableOfWordsAsKeyAndContainingDocsAsValue);

            return tableOfWordsAsKeyAndContainingDocsAsValue;
        }

        private static void IterateTokensToMergeTheIdenticalTokens(List<IWordOccurence> tokens,
            IDictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue)
        {
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
        }
    }
}