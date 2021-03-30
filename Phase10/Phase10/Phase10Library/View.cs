namespace Phase10Library
{
    public class View
    {
        public void Run(Settings settings)
        {
            IElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var myElasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.host);
            
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            
            var elasticResponseValidator = new ElasticResponseValidator();
            
            var searchController = new SearchController(myElasticClient, elasticResponseValidator, settings);
            var docsSearchingResultSet = searchController.SearchDocs(input);

            IResultPrinter resultPrinter = new ResultPrinter();
            resultPrinter.PrintResult(docsSearchingResultSet);
        }
    }
}