using System;
using Nest;
using Phase10Library;

namespace ImportRun
{
    class Program
    {
        static void Main(string[] args)
        {
            ElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            IElasticClient elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            var indexDefiner = new IndexDefiner(elasticClient);
            indexDefiner.CreateIndex(Indexes.DocsIndex);
        }
    }
}