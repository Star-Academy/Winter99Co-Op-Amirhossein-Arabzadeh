using System.Collections.Generic;
using Nest;

namespace Phase10Library
{
    public interface IMyElasticClient
    {
        ISearchResponse<Doc> GetSearchItemFromDb(string unsignedWord);
        
    }
}