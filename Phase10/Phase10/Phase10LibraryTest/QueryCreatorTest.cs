using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class QueryCreatorTest
    {
        private const string Field = "Content";
        [Fact]
        public void GetQueryContainer_ShouldReturnWantedSearchQuery_WhenParametersAreValid()
        {
            CreateSearchingWordsString(out var plusSignedWordString, out var minusSignedWordString,
                out var unsignedWordString);
            
            var trimmedExpectedQuery = ProvideTrimmedExpectedQuery(unsignedWordString, plusSignedWordString, minusSignedWordString);

            var trimmedActualQuery = ProvideTrimmedActualQuery(unsignedWordString, plusSignedWordString, minusSignedWordString);

            Assert.Equal(trimmedExpectedQuery, trimmedActualQuery);
        }

        private static string ProvideTrimmedActualQuery(StringBuilder unsignedWordString, StringBuilder plusSignedWordString,
            StringBuilder minusSignedWordString)
        {
            var queryCreator = new QueryCreator();
            var actualQuery = JsonSerializer
                .Serialize(queryCreator.GetQueryContainer(unsignedWordString,
                    plusSignedWordString, minusSignedWordString, Field));
            
            var trimmedActualQuery = string.Concat(actualQuery.Where(c => !Char.IsWhiteSpace(c)));
            
            return trimmedActualQuery;
        }

        private static string ProvideTrimmedExpectedQuery(StringBuilder unsignedWordString, StringBuilder plusSignedWordString,
            StringBuilder minusSignedWordString)
        {
            var query = CreateExpectedQueryContainer(unsignedWordString, plusSignedWordString, minusSignedWordString);

            var expectedQuery = JsonSerializer.Serialize(query);
            var trimmedExpectedQuery = string.Concat(expectedQuery.Where(c => !Char.IsWhiteSpace(c)));
            
            return trimmedExpectedQuery;
        }

        private static QueryContainer CreateExpectedQueryContainer(StringBuilder unsignedWordString, StringBuilder plusSignedWordString,
            StringBuilder minusSignedWordString)
        {
            QueryContainer query = new BoolQuery
            {
                Must = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = Field,
                        Query = unsignedWordString.ToString(),
                        Operator = Operator.And
                    }
                },
                Should = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = Field,
                        Query = plusSignedWordString.ToString(),
                        Operator = Operator.Or
                    }
                },
                MustNot = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = Field,
                        Query = minusSignedWordString.ToString(),
                        Operator = Operator.Or
                    }
                }
            };
            return query;
        }

        private static void CreateSearchingWordsString(out StringBuilder plusSignedWordString,
            out StringBuilder minusSignedWordString, out StringBuilder unsignedWordString)
        {
            unsignedWordString = new StringBuilder("ali hasan hossein");
            plusSignedWordString = new StringBuilder("sajad mohammad jafar");
            minusSignedWordString = new StringBuilder("moosa reza mahdi");
        }
    }
}