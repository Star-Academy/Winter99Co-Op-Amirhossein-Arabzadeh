using System;
using System.IO;

namespace Phase10Library
{
    public class FileReader
    {
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