using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    public class Course
    {
        public Course(int studentNumber, string lesson, float score)
        {
            StudentNumber = studentNumber;
            Lesson = lesson;
            Score = score;
        }

        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public float Score { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Course course &&
                   StudentNumber == course.StudentNumber &&
                   Lesson == course.Lesson &&
                   Score == course.Score;
        }
    }
}
