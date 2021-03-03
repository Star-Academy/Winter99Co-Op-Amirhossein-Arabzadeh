using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class IndexController : IIndexController
    {
        private readonly IHashTableCreator _hashTableCreator;
        private Dictionary<string, List<string>> Table { get; set; }

        public IndexController(IHashTableCreator hashTableCreator)
        {
            _hashTableCreator = hashTableCreator;
        }

        public async void ProcessDocs(string folderRelatedPath)
        {
            Table = _hashTableCreator.CreateHashTableOfWordsAsKeyAndContainingDocsAsValue(folderRelatedPath);
            using (var context = new InvertedIndexContext())
            {
                context.AddRange(Table);
                await context.SaveChangesAsync();
            }
        }

        public Dictionary<string, List<string>> GetInvertedIndexTable()
        {
            return new Dictionary<string, List<string>>(Table);
        }
    }
}