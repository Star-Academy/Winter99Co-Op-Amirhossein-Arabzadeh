using System;
using System.Collections.Generic;
using System.Linq;

namespace Phase10Library
{
    public class ResultPrinter : IResultPrinter
    {
        public void PrintResult(IEnumerable<string> docsSearchingResultSet)
        {
            Console.WriteLine(docsSearchingResultSet.Count());
            foreach (var doc in docsSearchingResultSet)
            {
                Console.WriteLine(doc);
            }
        }
    }
}