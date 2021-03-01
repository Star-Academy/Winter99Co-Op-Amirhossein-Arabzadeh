using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ITokenizer
    {
        public List<WordOccurrence> TokenizeFiles(IEnumerable<string> filePaths);

    }
}