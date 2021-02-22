﻿using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class IndexController : IIndexController
    {
        private IHashTableCreator _hashTableCreator;
        public static Dictionary<string, List<string>> Table { get; set; }

        public IndexController(IHashTableCreator hashTableCreator)
        {
            _hashTableCreator = hashTableCreator;
        }

        public void ProcessDocs(string folderRelatedPath)
        {
            IDictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue =
                _hashTableCreator.createHashTableOfWordsAsKeyAndContainingDocsAsValue(folderRelatedPath);
        }

        public Dictionary<string, List<string>> GetInvertedIndexTable()
        {
            return Table;
        }
    }
}