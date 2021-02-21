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
        [Fact]
        public void GetFilesNames_ShouldGetFilePathsInDirectory_WhenInputIsValid()
        {
            const string relativeDirectoryPath = "../../../../Resources/SmallEnglishData";
            
            string[] fileNames = {"58043", "58044"};
            var filesRelativePaths = fileNames.Select(s => relativeDirectoryPath+ "\\" + s );
            IFileNamesExtractor folderFileNamesExtractor = new FileNamesExtractor();
            Assert.Equal(filesRelativePaths, folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath));
        }

        [Fact]
        public void GetFilesNames_ShouldThrowDirectoryNotFoundException_WhenInputIsInvalid()
        {
            const string relativeDirectoryPath = "../../../../Resource/SmallEnglishData";
            
            IFileNamesExtractor folderFileNamesExtractor = new FileNamesExtractor();
            
            // Act
            Action action = () => folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath);

            // Assert
            Assert.Throws<DirectoryNotFoundException>(action);
        }
        [Theory]
        [InlineData("    ")]
        [InlineData("   ")]
        [InlineData("   \n")]
        [InlineData(null)]
        public void GetFilesNames_ShouldThrowArgumentNullException_WhenInputIsWhiteSpace(string path)
        { 
            string relativeDirectoryPath = path;
            
            IFileNamesExtractor folderFileNamesExtractor = new FileNamesExtractor();
            
            // Act
            Action action = () => folderFileNamesExtractor.GetFilesRelatedPaths(relativeDirectoryPath);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
        
        
    }

    
}