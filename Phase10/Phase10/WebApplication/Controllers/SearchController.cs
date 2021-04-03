using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Phase10Library;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SearchController : ControllerBase
    {
        private readonly Phase10Library.SearchController _searchController;

        public SearchController(ILogger<SearchController> logger, Phase10Library.SearchController searchController)
        {
            _searchController = searchController;
        }

        [HttpGet]
        public IEnumerable<string> Get([FromBody] string input)
        {
            var docsSearchingResultSet = _searchController.SearchDocs(input);

            return docsSearchingResultSet;
        }
    }
}