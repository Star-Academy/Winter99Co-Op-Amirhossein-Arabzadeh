using System.Collections.Generic;
using System.Text;
using Nest;

namespace Phase10Library
{
    public class QueryCreator
    {
        public QueryCreator()
        {
        }

        public QueryContainer GetQueryContainer(StringBuilder unsignedWordString, StringBuilder plusSignedWordString,
            StringBuilder minusSignedWordString, string field)
        {
            QueryContainer query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = field,
                        Query = unsignedWordString.ToString(),
                        Operator = Operator.And
                    }
                },
                Should = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = field,
                        Query = plusSignedWordString.ToString(),
                        Operator = Operator.Or
                    }
                },
                MustNot = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = field,
                        Query = minusSignedWordString.ToString(),
                        Operator = Operator.Or
                    }
                }
            };
            return query;
        }
    }
}