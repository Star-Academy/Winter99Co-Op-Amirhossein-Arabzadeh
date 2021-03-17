using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace Phase10Library
{
    //TODO: test this query
    public class MyElasticClient
    {
        private readonly ElasticClientFactory _elasticClientFactory = new ElasticClientFactory();
        private readonly IElasticClient _elasticClient;

        public MyElasticClient()
        {
            _elasticClient = _elasticClientFactory.CreateElasticClient("http://localhost:9200");
        }
    }
}