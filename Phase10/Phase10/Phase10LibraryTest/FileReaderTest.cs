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
        private const string WhiteSpace = "   ";
        private const string ProperFilepath = "properFilePath";
        private const int IndexOfFileNameStartInRelatedPath = 37;


        [Fact]
        public void GetDocs_ShouldThrowArgumentNullException_WhenParameterIsNull()
        {
            var fileReader = new FileReader();
            void Action() => fileReader.GetDocs((List<string>) null, IndexOfFileNameStartInRelatedPath);
            Assert.Throws<ArgumentNullException>(Action);
        }

        [Fact]
        public void GetDocs_ShouldThrowArgumentException_WhenParameterIsEmpty()
        {
            var filePaths = new List<string>();
            var fileReader = new FileReader();
            void Action() => fileReader.GetDocs(filePaths, IndexOfFileNameStartInRelatedPath);
            Assert.Throws<ArgumentException>(Action);
        }

        public static IEnumerable<object[]> GetDocsInvalidArguments = new List<object[]>
        {
            new object[] {new List<string>
            {
                WhiteSpace,
                ProperFilepath,
                null
            }},
            new object[] {new List<string>
            {
                null
            }},
            new object[] {new List<string>
            {
                WhiteSpace,
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
            const string filePath3 = "../../../GetContentTestFile3.txt";
            const string filePath2 = "../../../GetContentTestFile2.txt";
            const string filePath1 = "../../../GetContentTestFile1.txt";
            var fileReader = new FileReader();
            var filePaths = new List<string>
            {
                filePath1,
                filePath2,
                filePath3,
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