using System;
using System.Collections.Generic;
using System.Linq;

namespace Phase04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Decerialize decerialize = new Decerialize();
            List<Student> students = decerialize.ReadStudents();
            List<Lessons> scores = decerialize.ReadScores();

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
            for (int i = 0; i < 10; i++)
            {
                var student = orderByResult.ElementAt(i);
                Console.WriteLine(student.StudentFirstname + " " + student.StudentLastname + " " + student.StudentAverage);
            }

        }
    }
}
