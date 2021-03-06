using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class View : IView
    {
        public void Run(IIndexController indexController, InvertedIndexContext invertedIndexContext)
        {
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            ISearchController searchController = new SearchController(indexController, invertedIndexContext);
            var docsSearchingResultSet = searchController.SearchDocs(input);
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}