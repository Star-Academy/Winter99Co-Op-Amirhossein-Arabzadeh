using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    public class Importer<T> where T : class
    {
        private readonly IElasticClient _client;

        public Importer(IElasticClient myElasticClient)
        {
            _client = myElasticClient;
        }

        public void Import(IEnumerable<T> documents, string index)
        {
            var bulk = CreateBulk(documents, index); 
            _client.Bulk(bulk);
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