using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListOperator
    {
        List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord, Dictionary<string, List<string>> table);
        List<string> GetIntersectedUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result, Dictionary<string, List<string>> table);
        List<string> GetRemovedDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result, Dictionary<string, List<string>> table);
        List<string> GetRemovedDocsContainingMinusSignedWords(List<string> minusSignedWords, List<string> result, Dictionary<string, List<string>> table);
    }
}