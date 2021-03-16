using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    public interface IMyElasticClient2
    {
        ISearchResponse<Doc> GetSearchItemFromDb(string unsignedWord);
        IEnumerable<string> GetResultSetOfSearch(List<string> unsignedWords, List<string> plusSignedWords, List<string> minusSignedWords);
    }
}