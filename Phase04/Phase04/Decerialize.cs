using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phase04
{
    class Decerialize
    {
        public List<Student> ReadStudents() {
            string path = Path.GetFullPath("../../../../Resources/Students.txt");
            var read = File.ReadAllText(path);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(read);
            return students;
        }
        public List<Lessons> ReadScores()
        {
            string path = Path.GetFullPath("../../../../Resources/Scores.txt");
            var read = File.ReadAllText(path);
            List<Lessons> scores = JsonConvert.DeserializeObject<List<Lessons>>(read);
            return scores;
        }

    }
}
