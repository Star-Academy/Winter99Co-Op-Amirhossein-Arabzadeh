using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvertedIndexLibrary
{
    public class ListCalculator : IListCalculator
    {
        private readonly InvertedIndexContext _invertedIndexContext;

        public ListCalculator(InvertedIndexContext invertedIndexContext)
        {
            _invertedIndexContext = invertedIndexContext;
        }

        public ISet<string> GetDocsOfWordsList(List<string> words)
        {
            ValidateListAndDictionary(words);
            var setOfContainingDocsOfWords = IterateWordsListToTakeContainingDocsFromTable(words);
            return setOfContainingDocsOfWords;
        }

        private ISet<string> IterateWordsListToTakeContainingDocsFromTable(IEnumerable<string> partition)
        {
            var setOfContainingDocsOfPartitionTerms = new HashSet<string>();
            foreach (var term in partition)
            {
                var searchItemDocsList = GetSearchItemDocsList(term);
                if (searchItemDocsList is null)
                {
                    continue;
                }
                var docs = searchItemDocsList.Docs.Select(doc => doc.Id.ToString()).ToList();
                setOfContainingDocsOfPartitionTerms.UnionWith(docs);
            }

            return setOfContainingDocsOfPartitionTerms;

        }

        private SearchItem GetSearchItemDocsList(string term)
        {
            var searchItemDocsList = _invertedIndexContext.SearchingItems
                .Include(x => x.Docs)
                .FirstOrDefault(x => x.Term == term.ToLower());
            return searchItemDocsList;
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