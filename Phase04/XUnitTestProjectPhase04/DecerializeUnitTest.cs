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
            fileReader.Setup(x => x.getTextOfFile(It.IsAny<string>())).Returns("[{\"StudentNumber\": 1,\"FirstName\": \"Mahdi\",\"LastName\": \"Malverdi\"}]");

            Decerialize decerialize = new Decerialize(fileReader.Object);

            var expectedStudentsList = new ArrayList() { new Student("Mahdi", "Malverdi", 1) };


            Assert.Equal(expectedStudentsList, (ArrayList<Student>)decerialize.ReadStudents(It.IsAny<string>()));



        }
    }
}
