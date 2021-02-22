using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IPartitioner
    {
        void PartitionInputs(string searchingTerm, List<string> plusSignedInputWords, List<string> minusSignedInputWords, List<string> unSignedInputWords);
    }
}