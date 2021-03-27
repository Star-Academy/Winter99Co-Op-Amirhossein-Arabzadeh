using Phase10Library;

namespace ImportRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            var elasticResponseValidator = new ElasticResponseValidator();
            var indexDefiner = new IndexDefiner(elasticClient, elasticResponseValidator);
            indexDefiner.CreateIndex(Indexes.DocsIndex);
        }
    }
}