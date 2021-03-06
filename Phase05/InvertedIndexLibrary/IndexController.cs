using System.Collections.Generic;

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

            var searchItemsTable =
                _hashTableCreator.CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(folderRelatedPath);

            AddSearchItemsToDb(searchItemsTable, docsDictionary);

            _invertedIndexContext.SaveChanges();
        }

        private void AddSearchItemsToDb(Dictionary<string, List<string>> searchItemsTable, Dictionary<string, Doc> docsDictionary)
        {
            foreach (var (term, docs) in searchItemsTable)
            {
                var searchItem = GetNewSearchItem(term);
                AddDocsToSearchItem(docsDictionary, docs, searchItem);

                _invertedIndexContext.SearchingItems.Add(searchItem);
            }
        }

        private void AddDocsToSearchItem(Dictionary<string, Doc> docsDictionary, IEnumerable<string> docs, SearchItem searchItem)
        {
            foreach (var doc in docs)
            {
                var document = docsDictionary.GetValueOrDefault(doc) ?? GetNewDoc(docsDictionary, doc);
                searchItem.Docs.Add(document);
            }
        }

        private Doc GetNewDoc(IDictionary<string, Doc> docsDictionary, string doc)
        {
            var document = CreateNewDoc(doc);
            docsDictionary.Add(doc, document);
            return document;
        }

        private Doc CreateNewDoc(string doc)
        {
            var document = new Doc(doc);
            _invertedIndexContext.Docs.Add(document);
            return document;
        }

        private SearchItem GetNewSearchItem(string term)
        {
            var searchItem = new SearchItem
            {
                Term = term.ToLower(),
                Docs = new List<Doc>()
            };
            return searchItem;
        }
    }
}