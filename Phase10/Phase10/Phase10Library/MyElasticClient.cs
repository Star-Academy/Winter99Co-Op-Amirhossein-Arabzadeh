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
        private IElasticClient _elasticClient = new ElasticClient();
        public ISearchResponse<Doc> GetSearchItemFromDb(string unsignedWord)
        {
            var response = _elasticClient.Search<Doc>(s => s
                .Index(Indexes.DocsIndex)
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