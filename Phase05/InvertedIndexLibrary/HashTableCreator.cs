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

        public Dictionary<string, List<string>> createHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath)
        {
            List<IWordOccurence> tokens = _tokenizeController.TokenizeFilesTerms(relatedPath);
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue = 
                IterateTokensToMergeTheIdenticalTokens(tokens);

            return tableOfWordsAsKeyAndContainingDocsAsValue;
        }

        private Dictionary<string, List<string>> IterateTokensToMergeTheIdenticalTokens(List<IWordOccurence> tokens)
        {
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue =
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