using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Phase10Library;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SearchController
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get(string input)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            IElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var myElasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.Host);
            
            var elasticResponseValidator = new ElasticResponseValidator();
            
            var searchController = new Phase10Library.SearchController(myElasticClient, elasticResponseValidator, settings);
            var docsSearchingResultSet = searchController.SearchDocs(input);

            return docsSearchingResultSet;
        }
    }
}