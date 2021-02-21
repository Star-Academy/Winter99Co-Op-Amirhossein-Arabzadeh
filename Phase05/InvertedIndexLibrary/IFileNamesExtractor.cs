namespace InvertedIndexLibrary
{
    public interface IFileNamesExtractor
    {
        public abstract string[] GetFilesRelatedPaths(string folderRelativePath);

    }
}