using System;
using System.Collections;
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

        private void IterateWordsListToTakeContainingDocsFromTable(IEnumerable<string> partition, Dictionary<string, List<string>> table,
            ISet<string> setOfContainingDocsOfPartitionTerms)
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

        private void ValidateListAndDictionary(ICollection partition, Dictionary<string, List<string>> table)
        {
            if (IsNullOrEmpty(partition))
            {
                throw new ArgumentException(nameof(partition));
            }
            
            if (IsNullOrEmpty(table))
            {
                throw new ArgumentException(nameof(table));
            }
        }
        private bool IsNullOrEmpty(ICollection collection)
        {
            return collection== null ||  collection.Count == 0;
        }

        public List<string> MinusElementsOfSetFromList(ISet<string> set, List<string> list)
        {
            ValidateSetAndList(set, list);
            var returnList = (from term in list
                where !set.Contains(term)
                select term).ToList();
            return returnList;
        }

        private void ValidateSetAndList(ICollection<string> set, ICollection list)
        {
            if (IsSetOrListNullOrEmpty(set, list))
            {
                throw new ArgumentException("Set or List is either null or empty");
            }
        }

        private bool IsSetOrListNullOrEmpty(ICollection<string> set, ICollection list)
        {
            return set is null || list is null || set.Count == 0 || list.Count == 0;
        }

        public List<string> AndListWithSet(ISet<string> set, List<string> list)
        {
            ValidateSetAndList(set, list);
            var returnList = (from term in list
                where set.Contains(term)
                select term).ToList();
            return returnList;
        }
    }
}