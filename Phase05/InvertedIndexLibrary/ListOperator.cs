using System;
using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class ListOperator : IListOperator
    {
        private IListCalculator _listCalculator;

        public ListOperator(IListCalculator listCalculator)
        {
            _listCalculator = listCalculator;
        }

        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord, Dictionary<string, List<string>> table)
        {
            if (unsignedWord is null || table is null || unsignedWord.Trim().Equals("") || table.Count == 0)
            {
                throw new ArgumentException("unsignedWord or table are either null or empty");
            }
            List<string> setOfDocsContainingUnsignedWord = new List<string>();
            if (table.ContainsKey(unsignedWord))
            {
                setOfDocsContainingUnsignedWord.AddRange(table[unsignedWord]);
            }

            return setOfDocsContainingUnsignedWord;
        }

        public List<string> GetIntersectedUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(unSignedWords, result, table);
            List<string> tempResult = new List<string>(result);
            tempResult = IterateUnsignedWordsToIntersectDocsList(unSignedWords, table, tempResult);

            return tempResult;

        }

        private List<string> IterateUnsignedWordsToIntersectDocsList(List<string> unSignedWords, Dictionary<string, List<string>> table, List<string> tempResult)
        {
            foreach (string unSignedWord in unSignedWords)
            {
                if (table.ContainsKey(unSignedWord))
                {
                    List<string> docs = table[unSignedWord];
                    tempResult = (from doc in tempResult
                        where docs.Contains(doc)
                        select doc).ToList();
                }
            }

            return tempResult;
        }

        private void ValidateListsAndDictionary(List<string> words, List<string> result, Dictionary<string, List<string>> table)
        {
            if (AreListsOrDictionaryNullOrEmpty(words, result, table))
            {
                throw new ArgumentException("One or more of the parameters is either null or empty");
            }
        }

        private bool AreListsOrDictionaryNullOrEmpty(List<string> unSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            return unSignedWords is null || result is null || table is null || unSignedWords.Count == 0 || result.Count == 0 ||
                   table.Count == 0;
        }

        public List<string> GetDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(plusSignedWords, result, table);
            ISet<string> docsContainingPlusSignedWords =
                _listCalculator.CreateSetOfDifferentPartitions(plusSignedWords, table);

            List<string> tempResult = new List<string>(result);
            tempResult = (from doc in result
                where docsContainingPlusSignedWords.Contains(doc)
                select doc).ToList();
            return tempResult;
        }

        public List<string> GetRemovedDocsExcludingMinusSignedWords(List<string> minusSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(minusSignedWords, result, table);
            List<string> tempResult = new List<string>(result);
            ISet<string> minusSignedWordsContainingDocs =
                _listCalculator.CreateSetOfDifferentPartitions(minusSignedWords, table);
            tempResult = (from doc in tempResult
                where !(minusSignedWordsContainingDocs.Contains(doc))
                select doc).ToList();
            return tempResult;
        }
    }
}