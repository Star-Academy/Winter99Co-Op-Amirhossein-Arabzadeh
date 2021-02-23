using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace InvertedIndexLibrary
{
    public class FileNamesExtractor : IFileNamesExtractor
    {
        public string[] GetFilesRelatedPaths([NotNull] string folderRelativePath)
        {
            CheckIfPathIsWhiteSpaceOrNull(folderRelativePath);
            try
            {
                return Directory.GetFiles(folderRelativePath);
            }
            catch (Exception e)
            {
                throw new DirectoryNotFoundException("directory relative path is wrong");
            }
        }

        private void CheckIfPathIsWhiteSpaceOrNull(string folderRelativePath)
        {
            if (IsWhiteSpaceOrNull(folderRelativePath))
            {
                throw new ArgumentNullException("directory path is entered white space");
            }
        }

        private bool IsWhiteSpaceOrNull(string folderRelativePath)
        {
            return folderRelativePath is null || folderRelativePath.Trim().Equals("");
        }
    }
}