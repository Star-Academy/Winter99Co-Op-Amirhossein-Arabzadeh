using System.Collections.Generic;

namespace Phase10Library
{
    public interface IResultPrinter
    {
        void PrintResult(IEnumerable<string> docsSearchingResultSet);
    }
}