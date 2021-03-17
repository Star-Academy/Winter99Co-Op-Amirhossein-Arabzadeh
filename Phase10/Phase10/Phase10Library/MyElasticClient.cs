using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace Phase10Library
{
    //TODO: test this query
    public class MyElasticClient : IMyElasticClient
    {
        private readonly ElasticClientFactory _elasticClientFactory = new ElasticClientFactory();
        private readonly IElasticClient _elasticClient;

        public MyElasticClient()
        {
            _elasticClient = _elasticClientFactory.CreateElasticClient("http://localhost:9200");
        }

        public ISearchResponse<Doc> GetSearchItemFromDb(string unsignedWord)
        {
            var response = _elasticClient.Search<Doc>(s => s
                .Index(Indexes.DocsIndex)
                .Size(1000)
                .Query(q => q
                    .Bool(b => b
                        .Must(must => must
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(unsignedWord))))));
            return response;
        }
    }
}