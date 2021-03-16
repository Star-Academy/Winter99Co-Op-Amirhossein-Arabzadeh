using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace Phase10Library
{
    public class Importer<T> where T : class
    {
        private readonly IElasticClient client;

        public Importer(IElasticClient myElasticClient)
        {
            client = myElasticClient;
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