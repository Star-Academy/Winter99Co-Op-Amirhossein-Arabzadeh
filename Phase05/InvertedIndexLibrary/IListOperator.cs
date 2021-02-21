using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListOperator
    {
        List<String> intersectUnsignedWordsContainingDocs(List<String> result, List<String> unSignedWords);
        List<String> removeDocsWithoutPlusWords(List<String> plusSignedWords, List<String> result);
        List<String> removeDocsContainingMinusSignedWords(List<String> minusSignedWords, List<String> result);
    }
}