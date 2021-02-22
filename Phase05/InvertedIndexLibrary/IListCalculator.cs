using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListCalculator
    {
        ISet<string> CreateSetOfDifferentPartitions(List<string> partition, Dictionary<string, List<string>> table);
        List<string> MinusElementsOfSetFromList(ISet<string> set, List<string> list);
        List<string> AndListWithSet(ISet<string> set, List<string> list);
    }
}