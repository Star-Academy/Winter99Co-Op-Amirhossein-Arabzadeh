using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    public class Student : IStudent
    {
        public Student(string firstName, string lastName, int studentNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentNumber = studentNumber;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentNumber { get; set; }

    }
}
