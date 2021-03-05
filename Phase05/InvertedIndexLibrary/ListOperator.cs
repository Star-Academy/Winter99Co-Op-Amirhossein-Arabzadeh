using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvertedIndexLibrary
{
    public class ListOperator : IListOperator
    {
        private readonly IListCalculator _listCalculator;

        public ListOperator(IListCalculator listCalculator)
        {
            _listCalculator = listCalculator;
        }

        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord)
        {
            using (var context = new InvertedIndexContext())
            {
                ValidateStringAndDictionary(unsignedWord);
                var setOfDocsContainingUnsignedWord = new List<string>();
                // var searchItem = context.SearchingItems.FirstOrDefault(x => x.Id.Equals(unsignedWord));
                var searchItem = context.SearchingItems.
                    Include(d => d.Docs).FirstOrDefault(x => x.Id == unsignedWord);
                if (searchItem == null)
                {
                    return setOfDocsContainingUnsignedWord;
                }

                var searchItemDocs = searchItem.Docs;
                if (searchItemDocs.Count() != 0)
                {
                    var docsNames = new List<string>();
                    foreach (var doc in searchItemDocs)
                    {
                        docsNames.Add(doc.Id.ToString());
                    }
                    setOfDocsContainingUnsignedWord.AddRange(docsNames);
                }

                return setOfDocsContainingUnsignedWord;
            }
        }

        private void ValidateStringAndDictionary(string unsignedWord)
        {
            if (string.IsNullOrWhiteSpace(unsignedWord))
            {
                throw new ArgumentException("unsignedWord is null or empty");
            }
        }

        public List<string> GetIntersectedUnsignedWordsContainingDocs(List<string> unSignedWords, List<string> result)
        {
            ValidateListsAndDictionary(unSignedWords, result);
            var tempResult = IterateUnsignedWordsToIntersectDocsList(unSignedWords, result);
            return tempResult;
        }

        private List<string> IterateUnsignedWordsToIntersectDocsList(IEnumerable<string> unSignedWords, IEnumerable<string> result)
        {
            using (var context = new InvertedIndexContext())
            {
                var tempResult = new List<string>(result);
                
                foreach (var unSignedWord in unSignedWords)
                {
                    var listOfWordDoc = context.SearchingItems.Include(x=> x.Docs)
                        .FirstOrDefault(x => x.Id.Equals(unSignedWord));
                    if (listOfWordDoc == null)
                    {
                        continue;
                    }

                    var docs = new List<string>();
                    foreach (var doc in listOfWordDoc.Docs)
                    {
                        docs.Add(doc.Id.ToString());
                    }
                    tempResult = tempResult.Intersect(docs).ToList();
                    
                }
                return tempResult;
            }
        }

        private void ValidateListsAndDictionary(ICollection words, ICollection result)
        {
            if (AreListsOrDictionaryNullOrEmpty(words, result))
            {
                throw new ArgumentException("One or more of the parameters is either null or empty");
            }
        }

        private bool AreListsOrDictionaryNullOrEmpty(ICollection unSignedWords, ICollection result)
        {
            return unSignedWords is null || result is null || unSignedWords.Count == 0 || result.Count == 0;
        }

        public List<string> GetDocsWithoutPlusWords(List<string> plusSignedWords, List<string> result)
        {
            ValidateListsAndDictionary(plusSignedWords, result);
            var docsContainingPlusSignedWords =
                _listCalculator.GetDocsOfWordsList(plusSignedWords);
            
            return result.Intersect(docsContainingPlusSignedWords).ToList();
        }

        public List<string> GetDocsExcludingMinusSignedWords(List<string> minusSignedWords, List<string> result)
        {
            ValidateListsAndDictionary(minusSignedWords, result);
            var tempResult = new List<string>(result);
            var minusSignedWordsContainingDocs =
                _listCalculator.GetDocsOfWordsList(minusSignedWords);
            tempResult = tempResult.Except(minusSignedWordsContainingDocs).ToList();
            return tempResult;
        }
    }
}