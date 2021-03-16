using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Nest;
using Phase10Library;
using Index = System.Index;

namespace IndexRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var indexDefiner = new IndexDefiner();
            indexDefiner.CreateIndex(Indexes.DocsIndex);
            FileNamesExtractor fileNamesExtractor = new FileNamesExtractor();
            var filePaths = fileNamesExtractor.GetFilesRelatedPaths("../../../../Resources/BigEnglishData");
            FileReader fileReader = new FileReader();
            var docs = fileReader.GetDocs(filePaths);
            IndexDocs(docs);
        }
        private static void IndexDocs(IEnumerable<Doc> docs)
        {
            ElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var importer = new Importer<Doc>(elasticClientFactory);
            importer.Import(docs, Indexes.DocsIndex);
        }

    }
}