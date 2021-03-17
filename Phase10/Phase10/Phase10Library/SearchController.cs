using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest;

namespace Phase10Library
{
    public class SearchController
    {
        private readonly IPartitioner _partitioner;
        private List<string> _unsignedWords;
        private List<string> _plusSignedWords;
        private List<string> _minusSignedWords;
        private readonly IElasticClient _elasticClient;

        public SearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            _partitioner = new Partitioner();
            _unsignedWords = new List<string>();
            _minusSignedWords = new List<string>();
            _plusSignedWords = new List<string>();
        }

        public IEnumerable<string> SearchDocs(string input)
        {
            PartitionInputWords(input);
            var docsSearchingResultSet = GetResultSetFromElasticsearch(_unsignedWords,
                _plusSignedWords, _minusSignedWords);
            return docsSearchingResultSet;
        }

        private IEnumerable<string> GetResultSetFromElasticsearch(List<string> unsignedWords, List<string> plusSignedWords, List<string> minusSignedWords)
        {
            var elasticClient = new MyElasticClient();
            return GetResultSetOfSearch(unsignedWords,
                plusSignedWords, minusSignedWords);
        }
        //TODO: make this method simple
        private IEnumerable<string> GetResultSetOfSearch(IEnumerable<string> unsignedWords, IEnumerable<string> plusSignedWords, IEnumerable<string> minusSignedWords)
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

        private void PartitionInputWords(string input)
        {
            _unsignedWords = _partitioner.GetUnSignedWords(input);
            _minusSignedWords = _partitioner.GetSignedWords(input, "-");
            _plusSignedWords = _partitioner.GetSignedWords(input, "+");
        }
    }
}