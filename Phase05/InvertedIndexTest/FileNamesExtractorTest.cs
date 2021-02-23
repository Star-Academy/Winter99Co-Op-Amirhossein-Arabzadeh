using System;
using System.Collections.Generic;
using System.IO;
using InvertedIndexLibrary;
using Xunit;
using System.Linq;

namespace InvertedIndexTest
{
    public class FileNamesExtractorTest
    {
        private IFileNamesExtractor folderFileNamesExtractor;
        public FileNamesExtractorTest()
        { 
            folderFileNamesExtractor = new FileNamesExtractor();
        }

        [Fact]
        public void GetFilesNames_ShouldGetFilePathsInDirectory_WhenInputIsValid()
        {
            const string relativeDirectoryPath = "../../../../Resources/SmallEnglishData";
            string[] fileNames = {"58043", "58044"};
            var filesRelativePaths = fileNames.Select(s => relativeDirectoryPath+ "\\" + s );
            Assert.Equal(filesRelativePaths, folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath));
        }

        [Fact]
        public void GetFilesNames_ShouldThrowDirectoryNotFoundException_WhenInputIsInvalid()
        {
            const string relativeDirectoryPath = "../../../../Resource/SmallEnglishData";
            
            Action action = () => folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath);
            
            Assert.Throws<DirectoryNotFoundException>(action);
        }
        [Theory]
        [InlineData("    ")]
        [InlineData("   ")]
        [InlineData("   \n")]
        [InlineData(null)]
        public void GetFilesNames_ShouldThrowArgumentNullException_WhenInputIsWhiteSpaceOrNull(string relativePath)
        {
            Action action = () => folderFileNamesExtractor.GetFilesRelatedPaths(relativePath);
            Assert.Throws<ArgumentNullException>(action);
        }
        
        
    }

    
}