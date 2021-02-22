using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListCalculator
    {
        ISet<String> CreateSetOfDifferentPartitions(List<String> partition, Dictionary<String, List<String>> table);
        List<String> MinusElementsOfSetFromList(ISet<String> set, List<String> list);
        List<String> AndListWithSet(ISet<String> set, List<String> list);
    }
}