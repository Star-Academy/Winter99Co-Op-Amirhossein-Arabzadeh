using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class SearchController : ControllerBase
    {
        private readonly Phase10Library.SearchController _searchController;

        public SearchController(Phase10Library.SearchController searchController)
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