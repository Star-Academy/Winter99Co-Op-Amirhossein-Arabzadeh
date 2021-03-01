using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ISearchController
    {
        IEnumerable<string> SearchDocs(string input);
    }
}