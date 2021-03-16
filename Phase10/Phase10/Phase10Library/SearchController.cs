using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    public class SearchController
    {
        private readonly IPartitioner _partitioner;
        private readonly IListOperator _listOperator;
        private List<string> _unsignedWords;
        private List<string> _plusSignedWords;
        private List<string> _minusSignedWords;

        public SearchController(IMyElasticClient myElasticClient)
        {
            _partitioner = new Partitioner();
            IListCalculator listCalculator = new ListCalculator(myElasticClient);
            _listOperator = new ListOperator(listCalculator, myElasticClient);
            _unsignedWords = new List<string>();
            _minusSignedWords = new List<string>();
            _plusSignedWords = new List<string>();
        }

        public IEnumerable<string> SearchDocs(string input)
        {
            PartitionInputWords(input);
            var docsSearchingResultSet = new List<string>();
            docsSearchingResultSet = GetResultSetFromElasticsearh(_unsignedWords, _plusSignedWords, _minusSignedWords);
            return docsSearchingResultSet;
        }

        private List<string> GetResultSetFromElasticsearh(List<string> unsignedWords, List<string> plusSignedWords, List<string> minusSignedWords)
        {
            var elasticClient = new MyElasticClient();
            return elasticClient.GetResultSetOfSearch(unsignedWords,
                plusSignedWords, minusSignedWords);
        }

        private void PartitionInputWords(string input)
        {
            _unsignedWords = _partitioner.GetUnSignedWords(input);
            _minusSignedWords = _partitioner.GetSignedWords(input, "-");
            _plusSignedWords = _partitioner.GetSignedWords(input, "+");
        }
    }
}