using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class ListCalculator : IListCalculator
    {
        public ISet<string> CreateSetOfDifferentPartitions(List<string> partition, Dictionary<string, List<string>> table)
        {
            var setOfContainingDocsOfPartitionTerms = new HashSet<string>();
            foreach (var term in partition)
            {
                if (!table.ContainsKey(term))
                {
                    continue;
                }

                setOfContainingDocsOfPartitionTerms.UnionWith(table[term]);
            }

            return setOfContainingDocsOfPartitionTerms;
        }

        public List<string> MinusResultSet(ISet<string> anotherSet, List<string> result)
        {
            throw new System.NotImplementedException();
        }

        public List<string> AndResultSet(ISet<string> docs, List<string> result)
        {
            throw new System.NotImplementedException();
        }
    }
}