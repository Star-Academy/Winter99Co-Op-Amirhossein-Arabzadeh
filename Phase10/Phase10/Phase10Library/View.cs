using System;
using System.Linq;

namespace Phase10Library
{
    public class View
    {
        public void Run()
        {
            IInputGetter inputGetter = new InputGetter();
            var input = inputGetter.GetInput();
            var searchController = new SearchController();
            var docsSearchingResultSet = searchController.SearchDocs(input);
            Console.WriteLine(docsSearchingResultSet.Count());
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}