using System;
using Xunit;
using Phase04;
using Moq;
using System.Collections.Generic;
using System.Collections;

namespace XUnitTestProjectPhase04
{
    public class DecerializeUnitTest
    {
         
        [Fact]
        public void ReadStudents()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(x => x.GetTextOfFile(It.IsAny<string>())).Returns("[{\"StudentNumber\": 1,\"FirstName\": \"Mahdi\",\"LastName\": \"Malverdi\"}]");

            Deserializer deserializer = new Deserializer(fileReader.Object);

            var expectedStudentsList = new List<Student>() { new Student("Mahdi", "Malverdi", 1) };

            Assert.Equal(expectedStudentsList, deserializer.ReadStudents(It.IsAny<string>()));

        }
        [Fact]
        public void ReadScores()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(x => x.GetTextOfFile(It.IsAny<string>())).Returns("[{\"StudentNumber\": 1,\"Lesson\": \"DB\",\"Score\": 14.63433486}]");

            Deserializer deserializer = new Deserializer(fileReader.Object);

            var expectedStudentsList = new List<Course>() { new Course(1, "DB", (float)14.63433486) };

            Assert.Equal(expectedStudentsList, deserializer.ReadScores(It.IsAny<string>()));

        }
    }
}
