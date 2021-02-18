using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class Lessons
    {
        public int StudentNumber { get; set; }
        public string Lesson { get; set; }
        public float Score { get; set; }

        public Lessons(int studentNumber, string lesson, float point)
        {
            StudentNumber = studentNumber;
            Lesson = lesson;
            Score = point;
        }

      
    }
}
