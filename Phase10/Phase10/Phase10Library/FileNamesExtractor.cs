using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Phase10Library
{
    public interface IFileNamesExtractor
    {
        string[] GetFilesRelatedPaths([NotNull] string folderRelativePath);
    }

    public class FileNamesExtractor : IFileNamesExtractor
    {
        public string[] GetFilesRelatedPaths([NotNull] string folderRelativePath)
        {
            CheckIfPathIsWhiteSpaceOrNull(folderRelativePath);
            try
            {
                return Directory.GetFiles(folderRelativePath);
            }
            catch (DirectoryNotFoundException)
            {
                throw new DirectoryNotFoundException("directory relative path is wrong");
            }
        }

        private void CheckIfPathIsWhiteSpaceOrNull(string folderRelativePath)
        {
            if (string.IsNullOrWhiteSpace(folderRelativePath))
            {
                throw new ArgumentNullException("directory path is entered white space");
            }
        }
    }
}