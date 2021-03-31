using System.Diagnostics.CodeAnalysis;

namespace Phase10Library
{
    public interface IFileNamesExtractor
    {
        string[] GetFilesRelatedPaths([NotNull] string folderRelativePath);
    }
}