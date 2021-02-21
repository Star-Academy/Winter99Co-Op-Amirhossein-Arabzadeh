using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IHashTableCreator
    {
        public IDictionary<string, List<string>> createHashTableOfWordsAsKeyAndContainingDocsAsValue(string relatedPath);
    }
}