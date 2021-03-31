using System.Diagnostics.CodeAnalysis;

namespace InvertedIndexLibrary
{
    public interface IFileNamesExtractor
    {
        string[] GetFilesRelatedPaths([NotNull] string folderRelativePath);
    }
}