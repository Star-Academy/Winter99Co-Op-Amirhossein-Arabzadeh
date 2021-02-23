using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class View : IView
    {
        public void Run()
        {
            IInputGetter inputGetter = new InputGetter();
            string input = inputGetter.GetInput();
            ISearchController searchController = new SearchController();
            IEnumerable<string> docsSearchingResultSet = searchController.SearchDocs(input);
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}