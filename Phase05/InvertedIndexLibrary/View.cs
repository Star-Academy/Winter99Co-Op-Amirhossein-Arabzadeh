using System;
using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class View : IView
    {
        public void Run(IIndexController indexController, InvertedIndexContext invertedIndexContext)
        {
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            ISearchController searchController = new SearchController(invertedIndexContext);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}