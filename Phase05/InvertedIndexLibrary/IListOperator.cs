using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListOperator
    {
        List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord, List<string> result);
        List<string> IntersectUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result);
        List<string> RemoveDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result);
        List<string> RemoveDocsContainingMinusSignedWords(List<string> minusSignedWords, List<string> result);
    }
}