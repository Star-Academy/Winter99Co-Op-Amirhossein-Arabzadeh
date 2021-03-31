using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListOperator
    {
        List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord);
        List<string> GetIntersectedUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result);
        List<string> GetDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result);
        List<string> GetDocsExcludingMinusSignedWords(List<string> minusSignedWords, List<string> result);
    }
}