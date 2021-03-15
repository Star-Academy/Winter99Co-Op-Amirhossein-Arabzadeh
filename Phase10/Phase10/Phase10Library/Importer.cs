using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    internal class Importer<T> where T : class
    {
        private ElasticClientFactory _elasticClientFactory;
        private readonly IElasticClient client;

        public Importer(ElasticClientFactory elasticClientFactory)
        {
            _elasticClientFactory = elasticClientFactory;
            client = _elasticClientFactory.CreateElasticClient("http://localhost:9200/people-simple/_doc");
        }

        public void Import(IEnumerable<T> documents, string index)
        {
            var bulk = CreateBulk(documents, index);
            client.Bulk(bulk);
        }

        private BulkDescriptor CreateBulk(IEnumerable<T> documents, string index)
        {
            var bulkDescriptor = new BulkDescriptor();
            foreach (var document in documents)
            {
                bulkDescriptor.Index<T>(x => x
                    .Index(index)
                    .Document(document)
                );
            }
            return bulkDescriptor;
        }
    }
}