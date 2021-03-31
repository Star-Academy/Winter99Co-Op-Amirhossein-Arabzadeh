using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ITokenizer
    {
        List<WordOccurrence> TokenizeFiles(IEnumerable<string> filePaths);
    }
}