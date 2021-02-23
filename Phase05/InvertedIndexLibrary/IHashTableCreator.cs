using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IHashTableCreator
    {
        public Dictionary<string, List<string>> CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath);
    }
}