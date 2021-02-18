using System;
using System.Linq;

namespace Phase04
{
    public class OrderedEnumerableResultPrinter: IOrderedEnumerableResultPrinter
    {
        public void PrintResult(IOrderedEnumerable<StudentInfo> orderByResult)
        {
            if (orderByResult == null)
            {
                return;
            }

            for (var i = 0; i < 3; i++)
            {
                var student = orderByResult.ElementAt(i); 
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.AverageScore);
            }
        }
    }
}