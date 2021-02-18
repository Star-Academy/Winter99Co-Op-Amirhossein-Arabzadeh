using System;
using System.Collections.Generic;
using System.Linq;


namespace Phase04
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader();
            Decerialize decerialize = new Decerialize(fileReader);
            List<Student> students = decerialize.ReadStudents("../../../../Resources/Students.txt");
            List<Course> scores = decerialize.ReadScores("../../../../Resources/Scores.txt");

            var joinList = students.GroupJoin(scores,
                student => student.StudentNumber,
                score => score.StudentNumber,
                (student, studentPointes) => new
                {
                    StudentNo = student.StudentNumber,
                    StudentFirstname = student.FirstName,
                    StudentLastname = student.LastName,
                    StudentAverage = studentPointes.Average(course => course.Score),

                });

            var orderByResult = from s in joinList
                                orderby s.StudentAverage descending
                                select s;
            for (int i = 0; i < 3; i++)
            {
                var student = orderByResult.ElementAt(i);
                Console.WriteLine(student.StudentFirstname + " " + student.StudentLastname + " " + student.StudentAverage);
            }

        }


    }
}
