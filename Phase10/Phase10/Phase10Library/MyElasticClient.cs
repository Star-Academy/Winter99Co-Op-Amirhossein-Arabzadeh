using Nest;

namespace Phase10Library
{
    public interface IMyElasticClient
    {
        ISearchResponse<Doc> GetSearchItemFromDb(string unsignedWord);
    }

    //TODO: test this query
    public class MyElasticClient : IMyElasticClient
    {
        private ElasticClientFactory _elasticClientFactory = new ElasticClientFactory();
        private IElasticClient _elasticClient;

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