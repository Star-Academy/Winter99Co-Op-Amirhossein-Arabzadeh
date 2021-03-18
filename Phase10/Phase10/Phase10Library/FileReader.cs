using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phase10Library
{
    public class FileReader : IFileReader
    {
        public IEnumerable<Doc> GetDocs(IEnumerable<string> filePaths, int indexOfFileNameStartInRelatedPath)
        {
            var pathsArray = GetFilePathsArray(filePaths);
            ValidateFilePaths(pathsArray);
            return pathsArray.Select(filePath => new Doc(filePath.Substring(indexOfFileNameStartInRelatedPath),
                GetFileContent(filePath))).ToList();
        }

        private string[] GetFilePathsArray(IEnumerable<string> filePaths)
        {
            var pathsArray = filePaths as string[] ?? filePaths.ToArray();
            return pathsArray;
        }

        private void ValidateFilePaths(IEnumerable<string> filePaths)
        {
            if (filePaths is null)
            {
                throw new ArgumentNullException("filePaths");
            }

            if (filePaths.Count() == 0)
            {
                throw new ArgumentException("provided filePaths enumerable is empty");
            }

            foreach (var filePath in filePaths)
            {
                ValidateFilePath(filePath);
            }
        }

        private string GetFileContent(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                return content;
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Console.WriteLine(fileNotFoundException);
                throw new FileNotFoundException(fileNotFoundException.Message);
            }
            
        }

        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Path is null or empty");
            }
        }
    }
}