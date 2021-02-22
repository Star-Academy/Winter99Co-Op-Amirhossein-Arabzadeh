using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface ISearchController
    {
        List<string> SearchDocs(string input);
    }
}