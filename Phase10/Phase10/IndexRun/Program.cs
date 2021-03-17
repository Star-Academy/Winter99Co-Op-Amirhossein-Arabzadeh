using System.Collections.Generic;
using Phase10Library;

namespace IndexRun
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var fileNamesExtractor = new FileNamesExtractor();
            var filePaths = fileNamesExtractor.GetFilesRelatedPaths(Addresses.FolderRelativePath);
            var fileReader = new FileReader();
            var docs = fileReader.GetDocs(filePaths, 37);
            IndexDocs(docs);
        }
        private static void IndexDocs(IEnumerable<Doc> docs)
        {
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, Indexes.DocsIndex);
        }

    }
}