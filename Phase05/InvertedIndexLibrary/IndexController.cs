using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class IndexController : IIndexController
    {
        private readonly IHashTableCreator _hashTableCreator;
        //private Dictionary<string, List<string>> Table { get; set; }

        public IndexController(IHashTableCreator hashTableCreator)
        {
            _hashTableCreator = hashTableCreator;
        }

        public async void ProcessDocs(string folderRelatedPath)
        {
            using (var context = new InvertedIndexContext())
            {

                var searchItemsTable = _hashTableCreator.CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(folderRelatedPath);

            var searchItemsList = new List<SearchItem>();

            foreach (var item in searchItemsTable)
            {
                var searchItem = new SearchItem
                {
                    Id = item.Key,
                    Docs = new List<Doc>()
                };
                foreach(var doc in item.Value)
                {
                    var document = new Doc(int.Parse(doc));
                    searchItem.Docs.Add(document);
                    context.Docs.Add(document);
                    
                }

                //searchItemsList.Add(searchItem);
                    context.SearchingItems.Add(searchItem);
                }
                
                await context.SaveChangesAsync();
            }
        }

        //public Dictionary<string, List<string>> GetInvertedIndexTable()
        //{
        //    return new Dictionary<string, List<string>>(Table);
        //}
    }
}