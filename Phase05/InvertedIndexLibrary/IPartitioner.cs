using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IPartitioner
    {
        public List<string> GetWantedSignedWords(string searchingTerm, string sign);
        public List<string> GetUnSignedWords(string searchingTerm);
    }
}