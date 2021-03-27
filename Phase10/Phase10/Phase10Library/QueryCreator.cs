using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    public class QueryCreator
    {
        private readonly string _unsignedWordString;
        private readonly string _plusSignedWordString;
        private readonly string _minusSignedWordString;
        private readonly string _field;
        public QueryCreator(string unsignedWordString, string plusSignedWordString, string minusSignedWordString, string field)
        {
            _unsignedWordString = unsignedWordString;
            _plusSignedWordString = plusSignedWordString;
            _minusSignedWordString = minusSignedWordString;
            _field = field;
        }

        public QueryContainer GetQueryContainer()
        {
            QueryContainer query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    GetMustPartOfQuery()
                },
                Should = new List<QueryContainer>
                {
                    GetShouldPartOfQuery()
                },
                MustNot = new List<QueryContainer>
                {
                    GetMustNotPartOfQuery()
                }
            };
            return query;
        }

        private MatchQuery GetMustNotPartOfQuery()
        {
            return new MatchQuery
            {
                Field = _field,
                Query = _minusSignedWordString,
                Operator = Operator.Or
            };
        }

        private MatchQuery GetShouldPartOfQuery()
        {
            return new MatchQuery
            {
                Field = _field,
                Query = _plusSignedWordString,
                Operator = Operator.Or
            };
        }

        private MatchQuery GetMustPartOfQuery()
        {
            return new MatchQuery
            {
                Field = _field,
                Query = _unsignedWordString,
                Operator = Operator.And
            };
        }
    }
}