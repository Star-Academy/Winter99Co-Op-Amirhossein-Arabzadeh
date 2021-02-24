using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class ListOperator : IListOperator
    {
        private readonly IListCalculator _listCalculator;

        public ListOperator(IListCalculator listCalculator)
        {
            _listCalculator = listCalculator;
        }

        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord, Dictionary<string, List<string>> table)
        {
            ValidateStringAndDictionary(unsignedWord, table);
            List<string> setOfDocsContainingUnsignedWord = new List<string>();
            if (table.ContainsKey(unsignedWord))
            {
                setOfDocsContainingUnsignedWord.AddRange(table[unsignedWord]);
            }

            return setOfDocsContainingUnsignedWord;
        }

        private void ValidateStringAndDictionary(string unsignedWord, ICollection table)
        {
            if (IsStringOrDictionaryNullOrEmpty(unsignedWord, table))
            {
                throw new ArgumentException("unsignedWord or table are either null or empty");
            }
        }

        private bool IsStringOrDictionaryNullOrEmpty(string unsignedWord, ICollection table)
        {
            return unsignedWord is null || table is null || unsignedWord.Trim().Equals("") || table.Count == 0;
        }

        public List<string> GetIntersectedUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(unSignedWords, result, table);
            List<string> tempResult = IterateUnsignedWordsToIntersectDocsList(unSignedWords, table, result);
            return tempResult;
        }

        private List<string> IterateUnsignedWordsToIntersectDocsList(IEnumerable<string> unSignedWords, Dictionary<string, List<string>> table, IEnumerable<string> result)
        {
            List<string> tempResult = new List<string>(result);
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

        private void ValidateListsAndDictionary(ICollection words, ICollection result, ICollection table)
        {
            if (AreListsOrDictionaryNullOrEmpty(words, result, table))
            {
                throw new ArgumentException("One or more of the parameters is either null or empty");
            }
        }

        private bool AreListsOrDictionaryNullOrEmpty(ICollection unSignedWords, ICollection result, ICollection table)
        {
            return unSignedWords is null || result is null || table is null || unSignedWords.Count == 0 || result.Count == 0 ||
                   table.Count == 0;
        }

        public List<string> GetDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(plusSignedWords, result, table);
            var docsContainingPlusSignedWords =
                _listCalculator.GetDocsOfWordsList(plusSignedWords, table);

            var tempResult = new List<string>(result);
            tempResult = (from doc in result
                where docsContainingPlusSignedWords.Contains(doc)
                select doc).ToList();
            return tempResult;
        }

        public List<string> GetDocsExcludingMinusSignedWords(List<string> minusSignedWords, List<string> result, Dictionary<string, List<string>> table)
        {
            ValidateListsAndDictionary(minusSignedWords, result, table);
            List<string> tempResult = new List<string>(result);
            ISet<string> minusSignedWordsContainingDocs =
                _listCalculator.GetDocsOfWordsList(minusSignedWords, table);
            tempResult = (from doc in tempResult
                where !(minusSignedWordsContainingDocs.Contains(doc))
                select doc).ToList();
            return tempResult;
        }
    }
}