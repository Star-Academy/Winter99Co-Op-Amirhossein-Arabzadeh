using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class View : IView
    {
        public void run()
        {
            IInputGetter inputGetter = new InputGetter();
            string input = inputGetter.GetInput();
            ISearchController searchController = new SearchController();
            List<string> docsSearchingResultSet = searchController.SearchDocs(input);

        }
    }
}