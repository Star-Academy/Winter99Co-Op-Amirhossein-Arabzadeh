using System;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace Phase10Library
{
    public class View
    {
        public void Run(Settings settings)
        {
            IElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var myElasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.HttpLocalhost);
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            var elasticResponseValidator = new ElasticResponseValidator();
            var searchController = new SearchController(myElasticClient, elasticResponseValidator, settings);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            Console.WriteLine(docsSearchingResultSet.Count());
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}