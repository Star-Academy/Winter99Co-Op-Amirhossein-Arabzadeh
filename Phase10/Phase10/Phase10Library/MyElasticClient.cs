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


        //TODO: make this method simple
        public IEnumerable<string> GetResultSetOfSearch(List<string> unsignedWords, List<string> plusSignedWords, List<string> minusSignedWords)
        {
            var unsignedWordString = new StringBuilder();
            foreach (var unsignedWord in unsignedWords)
            {
                unsignedWordString.Append(" " + unsignedWord);
            }
            
            var plusSignedWordString =new StringBuilder();
            foreach (var plusSignedWord in plusSignedWords)
            {
                plusSignedWordString.Append(" " + plusSignedWord);
            }

            var minusSignedWordString = new StringBuilder();
            foreach (var minusSignedWord in minusSignedWords)
            {
                minusSignedWordString.Append(" " + minusSignedWord);
            }
            var response = _elasticClient.Search<Doc>(s => s
                .Index(Indexes.DocsIndex)
                .Size(1000)
                .Query(q => q
                    .Bool(b => b
                        .Must(must => must
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(unsignedWordString.ToString())
                                .Operator(Operator.And)
                                .Analyzer(Analyzers.NgramAnalyzer)
                                ))
                        .Should(should => should
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(plusSignedWordString.ToString())
                                .Operator(Operator.Or)
                                .Analyzer(Analyzers.NgramAnalyzer)
                                ))
                        .MustNot(must => must
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(minusSignedWordString.ToString())
                                .Operator(Operator.Or)
                                .Analyzer(Analyzers.NgramAnalyzer)
                                )))));

            return response.Documents.Select(doc => doc.Name).ToList();

        }
    }
}