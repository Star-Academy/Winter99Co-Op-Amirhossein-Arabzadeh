using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    public class Decerialize : IDecerialize
    {
        public IFileReader FileReader { get; set; }

        public Decerialize(IFileReader fileReader)
        {
            FileReader = fileReader;
        }

        public List<Student> ReadStudents(string relatedPath)
        {
            string read = FileReader.getTextOfFile(relatedPath);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(read);
            return students;
        }
        public List<Course> ReadScores(string relatedPath)
        {
            string read = FileReader.getTextOfFile(relatedPath);
            List<Course> scores = JsonConvert.DeserializeObject<List<Course>>(read);
            return scores;
        }


    }
}
