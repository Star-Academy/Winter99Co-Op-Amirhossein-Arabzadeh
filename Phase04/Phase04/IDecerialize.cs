using System.Collections.Generic;

namespace Phase04
{
    public interface IDecerialize
    {
        abstract IFileReader FileReader { get; set;}
        abstract List<Course> ReadScores(string relatedPath);
        abstract List<Student> ReadStudents(string relatedPath);
    }
}