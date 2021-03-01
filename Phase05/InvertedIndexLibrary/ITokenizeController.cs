using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ITokenizeController
    {
        List<WordOccurrence> TokenizeFilesTerms(string relatedPath);
    }
}