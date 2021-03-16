using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phase10Library
{
    public class FileReader
    {
        public List<Doc> GetDocs(IEnumerable<string> filePaths)
        {
            return filePaths.Select(filePath => new Doc(filePath.Substring(37), GetFileContent(filePath))).ToList();
        }
        public string GetFileContent(string filePath)
        {
            ValidateFilePath(filePath);
            try
            {
                var content = File.ReadAllText(filePath);
                return content;
            }
            catch (Exception fileNotFoundException)
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