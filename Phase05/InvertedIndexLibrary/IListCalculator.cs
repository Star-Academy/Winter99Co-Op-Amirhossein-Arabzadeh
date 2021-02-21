using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListCalculator
    {
        ISet<String> CreateSetOfDifferentPartitions(List<String> partition, Dictionary<String, List<String>> table);
        List<String> MinusResultSet(ISet<String> anotherSet, List<String> result);
        List<String> AndResultSet(ISet<String> docs, List<String> result);
    }
}