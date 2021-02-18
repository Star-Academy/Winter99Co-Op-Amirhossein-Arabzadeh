using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StudentNumber { get; set; }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
