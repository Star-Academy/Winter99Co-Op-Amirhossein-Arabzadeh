using System;
using System.Linq;
using Nest;

namespace Phase10Library
{
    public class View
    {
        public void Run(IMyElasticClient myElasticClient)
        {
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            SearchController searchController = new SearchController(myElasticClient);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}