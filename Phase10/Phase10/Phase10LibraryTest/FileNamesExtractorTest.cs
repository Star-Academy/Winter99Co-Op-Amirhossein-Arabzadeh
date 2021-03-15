using System;
using System.IO;
using System.Linq;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class FileNamesExtractorTest
    {
        private readonly IFileNamesExtractor _folderFileNamesExtractor;
        public FileNamesExtractorTest()
        { 
            _folderFileNamesExtractor = new FileNamesExtractor();
        }

        [Fact]
        public void GetFilesNames_ShouldGetFilePathsInDirectory_WhenInputIsValid()
        {
            const string relativeDirectoryPath = "../../../../Resources/SmallEnglishData";
            string[] fileNames = {"58043", "58044"};
            Assert.Equal(fileNames, from fileName in _folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath)
                select fileName.Substring(39));
        }

        [Fact]
        public void GetFilesNames_ShouldThrowDirectoryNotFoundException_WhenInputIsInvalid()
        {
            const string relativeDirectoryPath = "../../../../Resource/SmallEnglishData";
            
            Action action = () => _folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath);
            
            Assert.Throws<DirectoryNotFoundException>(action);
        }
        [Theory]
        [InlineData("    ")]
        [InlineData("   ")]
        [InlineData("   \n")]
        [InlineData(null)]
        public void GetFilesNames_ShouldThrowArgumentNullException_WhenInputIsWhiteSpaceOrNull(string relativePath)
        {
            Action action = () => _folderFileNamesExtractor.GetFilesRelatedPaths(relativePath);
            Assert.Throws<ArgumentNullException>(action);
        }
        
        
    }

    
}