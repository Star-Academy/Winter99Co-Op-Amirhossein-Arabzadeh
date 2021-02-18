using System.Linq;

namespace Phase04
{
    public interface IOrderedEnumerableResultPrinter
    { 
        void PrintResult(IOrderedEnumerable<StudentInfo> orderByResult);
    }
}