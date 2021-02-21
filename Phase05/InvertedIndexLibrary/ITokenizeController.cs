using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ITokenizeController
    {
        List<IWordOccurence> TokenizeFilesTerms(string relatedPath);
    }
}