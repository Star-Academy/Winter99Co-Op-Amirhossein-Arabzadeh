using System.Collections.Generic;

namespace Phase04
{
    public interface IBestStudentsByAverageGetter
    {
        IEnumerable<StudentInfo> GetBestStudentsInfos(IEnumerable<Student> students, List<Course> scores);
    }
}