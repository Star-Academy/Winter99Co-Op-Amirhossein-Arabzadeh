using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IIndexController
    {
        static Dictionary<string, List<string>> Table { get; set; }
        void ProcessDocs(string folderRelatedPath);
        Dictionary<string, List<string>> GetInvertedIndexTable();
    }
}