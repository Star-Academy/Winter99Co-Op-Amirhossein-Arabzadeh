using System;
using System.Diagnostics;
using System.IO;

namespace InvertedIndexLibrary
{
    public class FileNamesExtractor : IFileNamesExtractor
    {
        public string[] GetFilesRelatedPaths(string folderRelativePath)
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

        private static void CheckIfPathIsWhiteSpaceOrNull(string folderRelativePath)
        {
            if (folderRelativePath is null || folderRelativePath.Trim().Equals(""))
            {
                throw new ArgumentNullException("directory path is entered white space");
            }
        }
    }
}