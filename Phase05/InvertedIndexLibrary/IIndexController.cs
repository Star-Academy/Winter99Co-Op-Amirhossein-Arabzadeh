using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IIndexController
    {
        public static IDictionary<string, List<string>> Table { get; set; }
        public void ProcessDocs(string folderRelatedPath);
        public Dictionary<string, List<string>> GetInvertedIndexTable();
    }
}