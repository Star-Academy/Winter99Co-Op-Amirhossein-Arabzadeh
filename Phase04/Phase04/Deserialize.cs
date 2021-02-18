using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    public class Deserialize : IDeserialize
    {
        public IFileReader FileReader { get; set; }

        public Deserialize(IFileReader fileReader)
        {
            FileReader = fileReader;
        }

        public List<Student> ReadStudents(string relatedPath)
        {
            string read = FileReader.GetTextOfFile(relatedPath);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(read);
            return students;
        }
        public List<Course> ReadScores(string relatedPath)
        {
            string read = FileReader.GetTextOfFile(relatedPath);
            List<Course> scores = JsonConvert.DeserializeObject<List<Course>>(read);
            return scores;
        }


    }
}
