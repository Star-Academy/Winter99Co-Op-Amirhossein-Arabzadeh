using System;
using System.Collections.Generic;
using System.Linq;

namespace Phase04
{
    public class OrderedEnumerableResultPrinter: IOrderedEnumerableResultPrinter
    {
        public void PrintResult(IEnumerable<StudentInfo> orderByResult)
        {
            if (orderByResult == null)
            {
                throw new ArgumentException(nameof(orderByResult));
            }

            for (var i = 0; i < 3; i++)
            {
                var student = orderByResult.ElementAt(i); 
                Console.WriteLine(student);
            }
        }
    }
}