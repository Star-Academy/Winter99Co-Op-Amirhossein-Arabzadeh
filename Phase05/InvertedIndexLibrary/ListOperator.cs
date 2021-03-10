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
        private readonly InvertedIndexContext _invertedIndexContext;

        public ListOperator(IListCalculator listCalculator, InvertedIndexContext invertedIndexContext)
        {
            _invertedIndexContext = invertedIndexContext;
            _listCalculator = listCalculator;
        }

        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord)
        {
            ValidateStringAndDictionary(unsignedWord);
            var setOfDocsContainingUnsignedWord = new List<string>();

            var searchItem = GetSearchItemFromDb(unsignedWord);

            if (searchItem == null)
            {
                return setOfDocsContainingUnsignedWord;
            }

            var searchItemDocs = searchItem.Docs;
            
            if (searchItemDocs.Count == 0) return setOfDocsContainingUnsignedWord;

            var docsNames = searchItemDocs.Select(doc => doc.Id.ToString()).ToList();
            
            setOfDocsContainingUnsignedWord.AddRange(docsNames);

            return setOfDocsContainingUnsignedWord;
            
        }

        private SearchItem GetSearchItemFromDb(string unsignedWord)
        {
            var searchItem = _invertedIndexContext
                .SearchingItems.Include(d => d.Docs).FirstOrDefault(x => x.Term.ToLower() == unsignedWord);
            return searchItem;
        }

        private bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            return collection== null ||  collection.Count == 0;
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
            var tempResult = GetIntersectedDocsList(unSignedWords, result);
            return tempResult;
        }

        private List<string> GetIntersectedDocsList(IEnumerable<string> unSignedWords, IEnumerable<string> result)
        {

            var tempResult = new List<string>(result);

            return (from unSignedWord in unSignedWords
                select _invertedIndexContext.SearchingItems
                    .Include(x => x.Docs)
                    .FirstOrDefault(x => x.Term.Equals(unSignedWord))
                into searchItem
                where searchItem != null
                select searchItem.Docs.Select(doc => doc.Id.ToString()).ToList())
                .Aggregate(tempResult, (current, docs) => current.Intersect(docs).ToList());
        
        }

        private void ValidateListsAndDictionary(ICollection<string> words, ICollection<string> result)
        {
            if (IsNullOrEmpty(words))
            {
                throw new ArgumentException("One or more of the parameters is either null or empty");
            }

            if (IsNullOrEmpty(result))
            {
                throw new ArgumentException("One or more of the parameters is either null or empty");
            }
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