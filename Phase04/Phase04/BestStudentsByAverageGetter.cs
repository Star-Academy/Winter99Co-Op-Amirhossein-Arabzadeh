using System.Collections.Generic;
using System.Linq;

namespace Phase04
{ 
    public class BestStudentsByAverageGetter : IBestStudentsByAverageGetter
    {

        public IEnumerable<StudentInfo> GetBestStudentsInfos(IEnumerable<Student> students, List<Course> scores)
        {
            return GetStudentsContainingScores(students, scores);
        }
        
        private IEnumerable<StudentInfo> GetStudentsContainingScores(IEnumerable<Student> students, List<Course> scores)
        {
            var studentsWithScoresJoinedList = students.GroupJoin(scores,
                student => student.StudentNumber,
                score => score.StudentNumber,
                (student, studentPoints) => new StudentInfo(
                    studentPoints.Average(course => course.Score),
                    student.FirstName,
                    student.LastName
                )
            );
            return OrderByAverage(studentsWithScoresJoinedList).ToList();
        }

        private IEnumerable<StudentInfo> OrderByAverage(IEnumerable<StudentInfo> joinList)
        {
            var orderedByResultListOfStudentsInfos = from s in joinList
                orderby s.AverageScore descending
                select s;
            return orderedByResultListOfStudentsInfos;
        }
    }
}