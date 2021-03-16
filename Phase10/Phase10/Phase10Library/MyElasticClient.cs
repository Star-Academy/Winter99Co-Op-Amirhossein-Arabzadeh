using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public List<string> GetResultSetOfSearch(List<string> unsignedWords, List<string> plusSignedWords, List<string> minusSignedWords)
        {
            StringBuilder unsignedWordString = new StringBuilder();
            foreach (var unsignedWord in unsignedWords)
            {
                unsignedWordString.Append(" " + unsignedWord);
            }
            
            StringBuilder plusSignedWordString =new StringBuilder();
            foreach (var plusSignedWord in plusSignedWords)
            {
                plusSignedWordString.Append(" " + plusSignedWord);
            }

            StringBuilder minusSignedWordString = new StringBuilder();
            foreach (var minusSignedWord in minusSignedWords)
            {
                minusSignedWordString.Append(" " + minusSignedWord);
            }
            var response = _elasticClient.Search<Doc>(s => s
                .Index(Indexes.DocsIndex)
                .Query(q => q
                    .Bool(b => b
                        .Must(must => must
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(unsignedWordString.ToString())
                                .Analyzer(Analyzers.NgramAnalyzer)
                                ))
                        .Should(should => should
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(plusSignedWordString.ToString())
                                .Analyzer(Analyzers.NgramAnalyzer)
                                ))
                        .MustNot(must => must
                            .Match(match => match
                                .Field(p => p.Content)
                                .Query(minusSignedWordString.ToString())
                                .Analyzer(Analyzers.NgramAnalyzer)
                                )))));
            List<string> result = new List<string>();
            foreach (var doc in response.Documents)
            {
                result.Add(doc.Name);
            }

            return result;

        }
    }
}