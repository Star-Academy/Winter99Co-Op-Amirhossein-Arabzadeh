using System;
using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class ListCalculator : IListCalculator
    {
        public ISet<string> GetDocsOfWordsList(List<string> words, Dictionary<string, List<string>> table)
        {
            ValidateListAndDictionary(words, table);
            var setOfContainingDocsOfWords = new HashSet<string>();
            IterateWordsListToTakeContainingDocsFromTable(words, table, setOfContainingDocsOfWords);

            return setOfContainingDocsOfWords;
        }

        private void IterateWordsListToTakeContainingDocsFromTable(List<string> partition, Dictionary<string, List<string>> table,
            HashSet<string> setOfContainingDocsOfPartitionTerms)
        {
            foreach (var term in partition)
            {
                if (!table.ContainsKey(term))
                {
                    continue;
                }

                setOfContainingDocsOfPartitionTerms.UnionWith(table[term]);
            }
        }

        private void ValidateListAndDictionary(List<string> partition, Dictionary<string, List<string>> table)
        {
            if (IsListOrTableNullOrEmpty(partition, table))
            {
                throw new ArgumentException("One or more of the arguments are empty or null");
            }
        }

        private bool IsListOrTableNullOrEmpty(List<string> partition, Dictionary<string, List<string>> table)
        {
            return partition == null || table == null || partition.Count == 0 || table.Count == 0;
        }

        public List<string> MinusElementsOfSetFromList(ISet<string> set, List<string> list)
        {
            ValidateSetAndList(set, list);
            List<string> returnList = (from term in list
                where !set.Contains(term)
                select term).ToList();
            return returnList;
        }

        private void ValidateSetAndList(ISet<string> set, List<string> list)
        {
            if (IsSetOrListNullOrEmpty(set, list))
            {
                throw new ArgumentException("Set or List is either null or empty");
            }
        }

        private bool IsSetOrListNullOrEmpty(ISet<string> set, List<string> list)
        {
            return set is null || list is null || set.Count == 0 || list.Count == 0;
        }

        public List<string> AndListWithSet(ISet<string> set, List<string> list)
        {
            ValidateSetAndList(set, list);
            List<string> returnList = (from term in list
                where set.Contains(term)
                select term).ToList();
            return returnList;
        }
    }
}