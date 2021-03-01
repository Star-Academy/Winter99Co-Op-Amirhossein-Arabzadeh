using Xunit;
using Phase04;
using Moq;
using System.Collections.Generic;

namespace XUnitTestProjectPhase04
{
    public class DecerializerUnitTest
    {
         
        [Fact]
        public void ReadStudents()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(x => x.GetTextOfFile(It.IsAny<string>())).Returns("[{\"StudentNumber\": 1,\"FirstName\": \"Mahdi\",\"LastName\": \"Malverdi\"}]");

            var deserializer = new Deserializer(fileReader.Object);

            var expectedStudentsList = new List<Student>() { new Student("Mahdi", "Malverdi", 1) };

            Assert.Equal(expectedStudentsList, deserializer.ReadStudents(It.IsAny<string>()));

        }
        [Fact]
        public void ReadScores()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(x => x.GetTextOfFile(It.IsAny<string>())).Returns("[{\"StudentNumber\": 1,\"Lesson\": \"DB\",\"Score\": 14.63433486}]");

            var deserializer = new Deserializer(fileReader.Object);

            var expectedStudentsList = new List<Course>() { new Course(1, "DB", (float)14.63433486) };

            Assert.Equal(expectedStudentsList, deserializer.ReadScores(It.IsAny<string>()));

        }
    }
}
