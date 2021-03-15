using System;
using System.Collections.Generic;
using System.IO;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class FileReaderTest
    {
        public static IEnumerable<object[]> GetFileContentInvalidArguments = new List<object[]>
        {
            new object[] {"chert"},
            new object[] {"pert"},
        };

        [Theory]
        [MemberData(nameof(GetFileContentInvalidArguments))]
        public void GetFileContent_ShouldThrowFileNotFoundException_IfPathIsInvalid(string path)
        {
            var fileReader = new FileReader();
            void Action() => fileReader.GetFileContent(path);
            Assert.Throws<FileNotFoundException>(Action);
        }
        
        
        
        
        
        
        public static IEnumerable<object[]> GetFileContentNullOrEmptyArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {"    "},
        };

        [Theory]
        [MemberData(nameof(GetFileContentNullOrEmptyArguments))]
        public void GetFileContent_ShouldThrowArgumentException_IfPathIsNullOrEmpty(string path)
        {
            var fileReader = new FileReader();
            void Action() => fileReader.GetFileContent(path);
            Assert.Throws<ArgumentException>(Action);
        }

        [Fact]
        public void GetFileContent_ShouldReturnWholeFileContent_WhenPathIsValid()
        {
            var fileReader = new FileReader();
            Assert.Equal("ali, hasan hossein ....]][][}{}P{L{P\r\n",
                fileReader.GetFileContent("GetContentTestFile.txt"));
        }
    }
    
}