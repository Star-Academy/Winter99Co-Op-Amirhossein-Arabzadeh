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
            this._unsignedWordString = unsignedWordString;
            this._plusSignedWordString = plusSignedWordString;
            this._minusSignedWordString = minusSignedWordString;
            this._field = field;
        }

        public QueryContainer GetQueryContainer()
        {
            QueryContainer query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = _field,
                        Query = _unsignedWordString,
                        Operator = Operator.And
                    }
                },
                Should = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = _field,
                        Query = _plusSignedWordString,
                        Operator = Operator.Or
                    }
                },
                MustNot = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = _field,
                        Query = _minusSignedWordString,
                        Operator = Operator.Or
                    }
                }
            };
            return query;
        }
    }
}