using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phase04
{
    public class Deserializer : IDeserializer
    {
        private readonly IFileReader _fileReader;

        public Deserializer(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public List<Student> ReadStudents(string relatedPath)
        {
            string read = _fileReader.GetTextOfFile(relatedPath);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(read);
            return students;
        }
        public List<Course> ReadScores(string relatedPath)
        {
            string read = _fileReader.GetTextOfFile(relatedPath);
            List<Course> scores = JsonConvert.DeserializeObject<List<Course>>(read);
            return scores;
        }


    }
}
