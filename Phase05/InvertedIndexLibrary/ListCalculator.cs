using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvertedIndexLibrary
{
    public class ListCalculator : IListCalculator
    {
        public ISet<string> GetDocsOfWordsList(List<string> words)
        {
            ValidateListAndDictionary(words);
            var setOfContainingDocsOfWords = new HashSet<string>();
            IterateWordsListToTakeContainingDocsFromTable(words, setOfContainingDocsOfWords);

            return setOfContainingDocsOfWords;
        }

        private void IterateWordsListToTakeContainingDocsFromTable(IEnumerable<string> partition,
            ISet<string> setOfContainingDocsOfPartitionTerms)
        {
            using (var context = new InvertedIndexContext())
            {
                foreach (var term in partition)
                {
                    var searchItemDocsList = context.SearchingItems.Include(x => x.Docs)
                        .FirstOrDefault(x => x.Id == term);
                    if (searchItemDocsList is null)
                    {
                        continue;
                    }
                    var docs = new List<string>();
                    foreach (var doc in searchItemDocsList.Docs)
                    {
                        docs.Add(doc.Id.ToString());
                    }
                    setOfContainingDocsOfPartitionTerms.UnionWith(docs);
                }
            }
        }

        private void ValidateListAndDictionary(ICollection partition)
        {
            if (IsNullOrEmpty(partition))
            {
                throw new ArgumentException(nameof(partition));
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