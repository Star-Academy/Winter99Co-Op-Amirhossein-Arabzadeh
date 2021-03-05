using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class IndexController : IIndexController
    {
        private readonly IHashTableCreator _hashTableCreator;
        public IndexController(IHashTableCreator hashTableCreator)
        {
            _hashTableCreator = hashTableCreator;
        }

        public void ProcessDocs(string folderRelatedPath)
        {
            using (var context = new InvertedIndexContext())
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
                            context.Docs.Add(document);
                        }
                        searchItem.Docs.Add(document);
                    }

                    context.SearchingItems.Add(searchItem);

                }
                context.SaveChanges();
            }
        }
        
    }
}