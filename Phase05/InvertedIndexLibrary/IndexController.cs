using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class IndexController : IIndexController
    {
        private readonly IHashTableCreator _hashTableCreator;
        private readonly InvertedIndexContext _invertedIndexContext;
        
        public IndexController(IHashTableCreator hashTableCreator, InvertedIndexContext indexContext)
        {
            _hashTableCreator = hashTableCreator;
            _invertedIndexContext = indexContext;
        }

        public void ProcessDocs(string folderRelatedPath)
        {
            
                var docsDictionary = new Dictionary<string, Doc>();

                var searchItemsTable = _hashTableCreator.
                    CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(folderRelatedPath);

                foreach (var item in searchItemsTable)
                {
                    var searchItem = new SearchItem
                    {
                        Id = item.Key,
                        Docs = new List<Doc>()
                    };
                    foreach(var doc in item.Value)
                    {
                        var document = docsDictionary.GetValueOrDefault(doc);
                        if (document is null)
                        {
                            document = new Doc(int.Parse(doc));
                            docsDictionary.Add(doc, document);
                            _invertedIndexContext.Docs.Add(document);
                        }
                        searchItem.Docs.Add(document);
                    }

                    _invertedIndexContext.SearchingItems.Add(searchItem);

                }
                _invertedIndexContext.SaveChanges();
            
        }
        
    }
}