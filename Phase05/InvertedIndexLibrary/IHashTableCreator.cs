using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IHashTableCreator
    {
        Dictionary<string, List<string>> CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath);
    }
}