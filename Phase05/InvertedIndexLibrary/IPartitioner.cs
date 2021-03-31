using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IPartitioner
    {
        List<string> GetSignedWords(string searchingTerm, string sign);
        List<string> GetUnSignedWords(string searchingTerm);
    }
}