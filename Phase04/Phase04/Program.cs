using System;
using System.Collections.Generic;
using System.Linq;


namespace Phase04
{
    static class Program
    {
        static void Main(string[] args)
        {
            IBestStudentsByAverageGetter bestStudentsByAverageGetter = new BestStudentsByAverageGetter();
            
            var fileReader = new FileReader();
            var deserialize = new Deserializer(fileReader);
            var students = deserialize.ReadStudents("Students.txt");
            var scores = deserialize.ReadScores("Scores.txt");
            
            var bestStudents = bestStudentsByAverageGetter.GetBestStudentsInfos(students, scores);
            
            var resultPrinter = new OrderedEnumerableResultPrinter();
            resultPrinter.PrintResult(bestStudents);

        }
    }

    
    
}
