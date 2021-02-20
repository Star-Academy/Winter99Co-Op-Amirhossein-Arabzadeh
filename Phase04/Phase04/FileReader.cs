using System.IO;

namespace Phase04
{
    public class FileReader : IFileReader
    {
        public string GetTextOfFile(string relatedPath)
        {
            var read = File.ReadAllText(relatedPath);
            return read;
        }
    }
}
