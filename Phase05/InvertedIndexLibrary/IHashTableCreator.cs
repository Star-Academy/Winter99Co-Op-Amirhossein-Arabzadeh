using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IHashTableCreator
    {
        public Dictionary<string, List<string>> createHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath);
    }
}