using System.Collections.Generic;

namespace Phase04
{
    public interface IDeserialize
    {
        IFileReader FileReader { get; set;}
        List<Course> ReadScores(string relatedPath);
        List<Student> ReadStudents(string relatedPath);
    }
}