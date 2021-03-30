using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Phase10Library;

namespace IndexRun
{
    class Program
    {
        private const int IndexOfFileNameStartInRelatedPath = 37;

        private const string AppsettingsJsonFile = "appsettings.json";

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppsettingsJsonFile, false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            var fileNamesExtractor = new FileNamesExtractor();
            var filePaths = fileNamesExtractor.GetFilesRelatedPaths(settings.Addresses.FolderRelativePath);
            
            var fileReader = new FileReader();
            var docs = fileReader.GetDocs(filePaths, IndexOfFileNameStartInRelatedPath);
            
            IndexDocs(docs, settings);
        }

        private static void IndexDocs(IEnumerable<Doc> docs, Settings settings)
        {
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.Host);
            var importer = new Importer<Doc>(elasticClient);
            importer.Import(docs, settings.Indexes.DocsIndex);
        }

    }
}