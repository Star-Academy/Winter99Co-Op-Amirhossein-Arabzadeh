using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListCalculator
    {
        ISet<String> createSetOfDifferentModeledInputs(List<String> partition, Dictionary<String, List<String>> table);
        List<String> minusResultSet(ISet<String> anotherSet, List<String> result);
        List<String> andResultSet(ISet<String> docs, List<String> result);
    }
}