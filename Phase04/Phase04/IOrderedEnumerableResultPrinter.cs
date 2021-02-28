using System.Collections.Generic;
using System.Linq;

namespace Phase04
{
    public interface IOrderedEnumerableResultPrinter
    { 
        void PrintResult(IEnumerable<StudentInfo> orderByResult);
    }
}