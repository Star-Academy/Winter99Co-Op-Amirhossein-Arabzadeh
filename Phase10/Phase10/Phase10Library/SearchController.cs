using System.Collections.Generic;
using System.Linq;
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
        private readonly ElasticResponseValidator _elasticResponseValidator;
        private readonly Settings _settings;

        public SearchController(IElasticClient elasticClient, ElasticResponseValidator elasticResponseValidator, Settings settings)
        {
            _elasticClient = elasticClient;
            _elasticResponseValidator = elasticResponseValidator;
            _settings = settings;
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

        private IEnumerable<string> GetResultSetFromElasticsearch(IEnumerable<string> unsignedWords,
            IEnumerable<string> plusSignedWords, IEnumerable<string> minusSignedWords)
        {
            return GetResultSetOfSearch(unsignedWords,
                plusSignedWords, minusSignedWords);
        }
        private IEnumerable<string> GetResultSetOfSearch(IEnumerable<string> unsignedWords,
            IEnumerable<string> plusSignedWords, IEnumerable<string> minusSignedWords)
        {
            var plusSignedWordString = GetPlusSignedWordString(plusSignedWords);;
            var minusSignedWordString = GetMinusSignedWordString(minusSignedWords);
            var unsignedWordString = GetUnsignedWordString(unsignedWords);

            var response = GetResponseFromClient(unsignedWordString, plusSignedWordString, minusSignedWordString);
            _elasticResponseValidator.Validate(response);
            return response.Documents.Select(doc => doc.Name).ToList();

        }

        private ISearchResponse<Doc> GetResponseFromClient(string unsignedWordString, string plusSignedWordString,
            string minusSignedWordString)
        {
            var queryCreator = new QueryCreator(unsignedWordString, plusSignedWordString, minusSignedWordString,
                "Content");
            var query = queryCreator.GetQueryContainer();
            var response = _elasticClient.Search<Doc>(s => s
                .Index(_settings.Indexes.DocsIndex)
                .Size(1000)
                .Query(q => query));
            _elasticResponseValidator.Validate(response);
            return response;
        }

        private string GetMinusSignedWordString(IEnumerable<string> minusSignedWords)
        {
            var minusSignedWordString = "";
            foreach (var minusSignedWord in minusSignedWords)
            {
                minusSignedWordString.Concat(" " + minusSignedWord);
            }

            return minusSignedWordString;
        }

        private string GetPlusSignedWordString(IEnumerable<string> plusSignedWords)
        {
            var plusSignedWordString = "";
            foreach (var plusSignedWord in plusSignedWords)
            {
                plusSignedWordString.Concat(" " + plusSignedWord);
            }

            return plusSignedWordString;
        }

        private string GetUnsignedWordString(IEnumerable<string> unsignedWords)
        {
            var unsignedWordString = "";
            foreach (var unsignedWord in unsignedWords)
            {
                unsignedWordString.Concat(" " + unsignedWord);
            }

            return unsignedWordString;
        }

        private void PartitionInputWords(string input)
        {
            _unsignedWords = _partitioner.GetUnSignedWords(input);
            _minusSignedWords = _partitioner.GetSignedWords(input, "-");
            _plusSignedWords = _partitioner.GetSignedWords(input, "+");
        }
    }
}