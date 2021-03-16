using System.Collections.Generic;
using Nest;
using Phase10Library;

namespace IndexRun
{
    class Program
    {
        static void Main(string[] args)
        {
            FileNamesExtractor fileNamesExtractor = new FileNamesExtractor();
            var filePaths = fileNamesExtractor.GetFilesRelatedPaths("../../../../Resources/BigEnglishData");
            FileReader fileReader = new FileReader();
            var docs = fileReader.GetDocs(filePaths);
            IndexDocs(docs);
        }
        private static void IndexDocs(IEnumerable<Doc> docs)
        {
            ElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            IElasticClient elasticClient = elasticClientFactory.CreateElasticClient("http://localhost:9200");
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, Indexes.DocsIndex);
        }

    }
}