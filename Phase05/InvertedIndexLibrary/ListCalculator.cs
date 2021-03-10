using System;
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
            var setOfContainingDocsOfWords = GetContainingDocsFromTable(words);
            return setOfContainingDocsOfWords;
        }

        private ISet<string> GetContainingDocsFromTable(IEnumerable<string> partition)
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

        private void ValidateListAndDictionary(ICollection<string> partition)
        {
            if (IsNullOrEmpty(partition))
            {
                throw new ArgumentException(nameof(partition));
            }
            
        }
        private bool IsNullOrEmpty<T>(ICollection<T> collection)
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

        private void ValidateSetAndList(ICollection<string> set, ICollection<string> list)
        {
            if (IsNullOrEmpty(set))
            {
                throw new ArgumentException("Set or List is either null or empty");
            }

            if (IsNullOrEmpty(list))
            {
                throw new ArgumentException("Set or List is either null or empty");
            }
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