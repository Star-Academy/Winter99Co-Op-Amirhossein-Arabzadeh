using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class View : IView
    {
        public void Run(IIndexController indexController)
        {
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            ISearchController searchController = new SearchController(indexController);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}