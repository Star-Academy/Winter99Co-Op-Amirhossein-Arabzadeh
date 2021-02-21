using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ITokenizer
    {
        public List<IWordOccurence> TokenizeFiles(IEnumerable<string> filePaths);

    }
}