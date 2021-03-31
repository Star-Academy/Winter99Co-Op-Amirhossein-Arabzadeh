using System.Collections.Generic;

namespace Phase10Library
{
    public interface IPartitioner
    {
        List<string> GetSignedWords(string searchingTerm, string sign);
        List<string> GetUnSignedWords(string searchingTerm);
    }
}