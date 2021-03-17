using System;
using System.Linq;

namespace Phase10Library
{
    public class View
    {
        public void Run()
        {
            IElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var myElasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            var searchController = new SearchController(myElasticClient);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            Console.WriteLine(docsSearchingResultSet.Count());
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}