using System.Collections.Generic;

namespace Phase04
{
    public interface IDeserializer
    {
        List<Course> ReadScores(string relatedPath);
        List<Student> ReadStudents(string relatedPath);
    }
}