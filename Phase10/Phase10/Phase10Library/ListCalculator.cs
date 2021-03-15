using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Phase10Library
{
    public interface IListCalculator
    {
        ISet<string> GetDocsOfWordsList(List<string> words);
        List<string> MinusElementsOfSetFromList(ISet<string> set, List<string> list);
        List<string> AndListWithSet(ISet<string> set, List<string> list);
    }

    public class ListCalculator : IListCalculator
    {
        private IMyElasticClient _client;

        public ListCalculator(IMyElasticClient client)
        {
            _client = client;
        }
        //TODO: this function should not be in this class
        public ISet<string> GetDocsOfWordsList(List<string> words)
        {
            ValidateListAndDictionary(words);
            var setOfContainingDocsOfWords = GetContainingDocsFromTable(words);
            return setOfContainingDocsOfWords;
        }

        //TODO: this function should not be in this class
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
                var docsIds = searchItemDocsList.Documents
                    .Select(responseDocument => responseDocument.Id).ToList();
                
                setOfContainingDocsOfPartitionTerms.UnionWith(docsIds);

            }
            return setOfContainingDocsOfPartitionTerms;
        }

        //TODO: this function should not be in this class
        private ISearchResponse<Doc> GetSearchItemDocsList(string term)
        {
            var response = _client.GetSearchItemFromDb(term);
            return response;
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