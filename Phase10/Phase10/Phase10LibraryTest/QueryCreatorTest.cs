using System;
using System.Collections.Generic;
using System.Linq;
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

        private static string ProvideTrimmedActualQuery(string unsignedWordString, string plusSignedWordString,
            string minusSignedWordString)
        {
            var queryCreator = new QueryCreator(unsignedWordString,
                plusSignedWordString, minusSignedWordString, Field);
            var actualQuery = JsonSerializer
                .Serialize(queryCreator.GetQueryContainer());
            
            var trimmedActualQuery = string.Concat(actualQuery.Where(c => !Char.IsWhiteSpace(c)));
            
            return trimmedActualQuery;
        }

        private static string ProvideTrimmedExpectedQuery(string unsignedWordString, string plusSignedWordString,
            string minusSignedWordString)
        {
            var query = CreateExpectedQueryContainer(unsignedWordString, plusSignedWordString, minusSignedWordString);

            var expectedQuery = JsonSerializer.Serialize(query);
            var trimmedExpectedQuery = string.Concat(expectedQuery.Where(c => !Char.IsWhiteSpace(c)));
            
            return trimmedExpectedQuery;
        }

        private static QueryContainer CreateExpectedQueryContainer(string unsignedWordString, string plusSignedWordString,
            string minusSignedWordString)
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
                        Query = plusSignedWordString,
                        Operator = Operator.Or
                    }
                },
                MustNot = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = Field,
                        Query = minusSignedWordString,
                        Operator = Operator.Or
                    }
                }
            };
            return query;
        }

        private static void CreateSearchingWordsString(out string plusSignedWordString,
            out string minusSignedWordString, out string unsignedWordString)
        {
            unsignedWordString = "ali hasan hossein";
            plusSignedWordString = "sajad mohammad jafar";
            minusSignedWordString = "moosa reza mahdi";
        }
    }
}