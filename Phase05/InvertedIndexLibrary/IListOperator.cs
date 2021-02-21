using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListOperator
    {
        List<String> IntersectUnsignedWordsContainingDocs(List<String> result, List<String> unSignedWords);
        List<String> RemoveDocsWithoutPlusWords(List<String> plusSignedWords, List<String> result);
        List<String> RemoveDocsContainingMinusSignedWords(List<String> minusSignedWords, List<String> result);
    }
}