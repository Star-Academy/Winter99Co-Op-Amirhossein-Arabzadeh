using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class ListOperator : IListOperator
    {
        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord, List<string> result)
        {
            throw new System.NotImplementedException();
        }

        public List<string> IntersectUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result)
        {
            throw new System.NotImplementedException();
        }

        public List<string> RemoveDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result)
        {
            throw new System.NotImplementedException();
        }

        public List<string> RemoveDocsContainingMinusSignedWords(List<string> minusSignedWords, List<string> result)
        {
            throw new System.NotImplementedException();
        }
    }
}