﻿using System.Collections.Generic;
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
        private readonly QueryCreator _queryCreator;
        private readonly ElasticResponseValidator _elasticResponseValidator;

        public SearchController(IElasticClient elasticClient, QueryCreator queryCreator, ElasticResponseValidator elasticResponseValidator)
        {
            _elasticClient = elasticClient;
            _queryCreator = queryCreator;
            _elasticResponseValidator = elasticResponseValidator;
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
            CreateWordStrings(unsignedWords, plusSignedWords, minusSignedWords, out var plusSignedWordString,
                out var minusSignedWordString, out var unsignedWordString);

            var response = GetResponseFromClient(unsignedWordString, plusSignedWordString, minusSignedWordString);
            _elasticResponseValidator.Validate(response);
            return response.Documents.Select(doc => doc.Name).ToList();

        }

        private ISearchResponse<Doc> GetResponseFromClient(string unsignedWordString, string plusSignedWordString,
            string minusSignedWordString)
        {
            var query = _queryCreator.GetQueryContainer(unsignedWordString, plusSignedWordString, minusSignedWordString,
                "Content");
            var response = _elasticClient.Search<Doc>(s => s
                .Index(Indexes.DocsIndex)
                .Size(1000)
                .Query(q => query));
            _elasticResponseValidator.Validate(response);
            return response;
        }

        

        private void CreateWordStrings(IEnumerable<string> unsignedWords, IEnumerable<string> plusSignedWords,
            IEnumerable<string> minusSignedWords, out string plusSignedWordString, out string minusSignedWordString,
            out string unsignedWordString)
        {
            unsignedWordString = GetUnsignedWordString(unsignedWords);

            plusSignedWordString = GetPlusSignedWordString(plusSignedWords);

            minusSignedWordString = GetMinusSignedWordString(minusSignedWords);
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