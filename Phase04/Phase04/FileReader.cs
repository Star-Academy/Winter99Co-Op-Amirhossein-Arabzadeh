using System.IO;

namespace Phase04
{
    public class FileReader : IFileReader
    {
        public string GetTextOfFile(string relatedPath)
        {
            string path = Path.GetFullPath(relatedPath);
            var read = File.ReadAllText(path);
            return read;
        }
    }
}
