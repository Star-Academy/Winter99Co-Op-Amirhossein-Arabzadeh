using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Phase10Library;
using Xunit;
using Moq;

namespace Phase10LibraryTest
{
    public class FileReaderTest
    {
        [Fact]
        public void GetDocs_ShouldThrowArgumentNullException_WhenParameterIsNull()
        {
            var fileReader = new FileReader();
            void Action() => fileReader.GetDocs((List<string>) null, 37);
            Assert.Throws<ArgumentNullException>(Action);
        }
        
        [Fact]
        public void GetDocs_ShouldThrowArgumentException_WhenParameterIsEmpty()
        {
            var filePaths = new List<string>();
            var fileReader = new FileReader();
            void Action() => fileReader.GetDocs(filePaths, 37);
            Assert.Throws<ArgumentException>(Action);
        }
        public static IEnumerable<object[]> GetDocsInvalidArguments = new List<object[]>
        {
            new object[] {new List<string>
            {
                "   ",
                "properFilePath",
                null
            }},
            new object[] {new List<string>
            {
                null
            }},
            new object[] {new List<string>
            {
                "   ",
            }},
        };
        [Theory]
        [MemberData(nameof(GetDocsInvalidArguments))]
        public void GetDocs_ShouldThrowArgumentException_WhenOneOrMoreFilePathIsEmpty(IEnumerable<string> filePaths)
        {
            var fileReader = new FileReader();
            void Action() => fileReader.GetDocs(filePaths, It.IsAny<int>());
            Assert.Throws<ArgumentException>(Action);
        }

        [Fact]
        public void GetDocs_ShouldReturnWantedNumberOfDocs_WhenParameterIsValid()
        {
            var fileReader = new FileReader();
            var filePaths = new List<string>
            {
                "../../../GetContentTestFile1.txt",
                "../../../GetContentTestFile2.txt",
                "../../../GetContentTestFile3.txt",
            };
            var docs = fileReader.GetDocs(filePaths, 8);
            Assert.Equal(3, docs.Count());
        }
        
        [Fact]
        public void GetDocs_ShouldReturnFileNotFoundException_WhenParameterHasNotExistingFilePaths()
        {
            var fileReader = new FileReader();
            var filePaths = new List<string>
            {
                "../../../NotExistingFile",
            };
            void Action() => fileReader.GetDocs(filePaths, It.IsAny<int>());
            Assert.Throws<FileNotFoundException>(Action);
        }
        

    }
    
}