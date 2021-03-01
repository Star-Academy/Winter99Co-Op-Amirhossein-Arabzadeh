using System.Collections;
using System.Collections.Generic;
using Phase04;
using Xunit;

namespace XUnitTestProjectPhase04
{
    public class BestStudentsByAverageGetterTest
    {
        [Fact]
        public void GetBestStudentsInfos_ShouldReturnBestStudentsOrderedByAverage_WhenParametersAreValid()
        {
            BestStudentsByAverageGetter bestStudentsByAverageGetter = new BestStudentsByAverageGetter();
            List<Student> students = new List<Student>
            {
                new Student("ali", "rezaei", 98100000),
                new Student("reza", "rezaei", 98100001),
                new Student("asghar", "rezaei", 98100002),
                new Student("javad", "rezaei", 98100003),
                new Student("akbar", "rezaei", 98100004),
            };
            List<Course> scores = new List<Course>
            {
                new Course(98100000, "DS", 12),
                new Course(98100000, "DB", 10),
                new Course(98100000, "AP", 12),
                new Course(98100001, "AP", 12),
                new Course(98100002, "AP", 20),
                new Course(98100003, "AP", 19),
                new Course(98100003, "DB", 18),
                new Course(98100004, "DB", 17),
            };
            var sortedStudentsByAverage = new List<StudentInfo>
            {
                new StudentInfo(20, "asghar", "rezaei"),
                new StudentInfo(18.5, "javad", "rezaei"),
                new StudentInfo(17, "akbar", "rezaei"),
                new StudentInfo(12, "reza", "rezaei"),
                new StudentInfo(11.333333015441895, "ali", "rezaei"),
            };
            Assert.Equal(sortedStudentsByAverage, bestStudentsByAverageGetter.GetBestStudentsInfos(students,
                scores));
        }
    }
}