using System.Collections.Generic;

namespace Phase10Library
{
    public interface IFileReader
    {
        IEnumerable<Doc> GetDocs(IEnumerable<string> filePaths, int indexOfFileNameStartInRelatedPath);
    }
}