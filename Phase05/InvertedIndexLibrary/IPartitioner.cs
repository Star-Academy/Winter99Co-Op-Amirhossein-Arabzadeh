using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IPartitioner
    {
        List<string> GetWantedSignedWords(string searchingTerm, string sign);
        List<string> GetUnSignedWords(string searchingTerm);
    }
}