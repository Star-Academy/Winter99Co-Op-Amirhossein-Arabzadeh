using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IListCalculator
    {
        ISet<string> GetDocsOfWordsList(List<string> words);
        List<string> MinusElementsOfSetFromList(ISet<string> set, List<string> list);
        List<string> AndListWithSet(ISet<string> set, List<string> list);
    }
}