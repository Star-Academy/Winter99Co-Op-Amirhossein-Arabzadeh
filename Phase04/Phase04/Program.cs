using System;
using System.Collections.Generic;
using System.Linq;


namespace Phase04
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var deserialize = new Deserialize(fileReader);
            var students = deserialize.ReadStudents("../../../../Resources/Students.txt");
            var scores = deserialize.ReadScores("../../../../Resources/Scores.txt");

            var joinList = JoinScoresWithStudents(students, scores);
            var orderByResult = OrderByAverage(joinList);
            
            var resultPrinter = new OrderedEnumerableResultPrinter();
            resultPrinter.PrintResult(orderByResult);

        }

        private static IOrderedEnumerable<StudentInfo> OrderByAverage(IEnumerable<StudentInfo> joinList)
        {
            var orderByResult = from s in joinList
                orderby s.AverageScore descending
                select s;
            return orderByResult;
        }


        private static IEnumerable<StudentInfo> JoinScoresWithStudents(List<Student> students, List<Course> scores)
        {
            var joinList = students.GroupJoin(scores,
                student => student.StudentNumber,
                score => score.StudentNumber,
                (student, studentPoints) => new StudentInfo(
                    studentPoints.Average(course => course.Score),
                    student.FirstName,
                    student.LastName
                    )
                );
            return joinList;
        }
    }

    
    
}
