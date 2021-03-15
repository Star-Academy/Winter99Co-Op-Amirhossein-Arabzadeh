using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Phase10Library
{
    public class ListOperator
    {
        private readonly IListCalculator _listCalculator;
        private readonly IMyElasticClient _myElasticClient;
        

        public ListOperator(IListCalculator listCalculator, IMyElasticClient myElasticClient)
        {
            _myElasticClient = myElasticClient;
            _listCalculator = listCalculator;
        }

        public List<string> InitializeResultSetByFirstUnsignedInputWordDocs(string unsignedWord)
        {
            ValidateStringAndDictionary(unsignedWord);
            var setOfDocsContainingUnsignedWord = new List<string>();

            var searchItemDocs = _myElasticClient.GetSearchItemFromDb(unsignedWord);

            if (searchItemDocs == null)
            {
                return setOfDocsContainingUnsignedWord;
            }

            if (searchItemDocs.Documents.Count == 0) return setOfDocsContainingUnsignedWord;
            
            var docsIds = searchItemDocs.Documents.Select(document => document.Id).ToList();

            setOfDocsContainingUnsignedWord.AddRange(docsIds);

            return setOfDocsContainingUnsignedWord;
            
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

            foreach (var unSignedWord in unSignedWords)
            {
                var searchItem = _myElasticClient.GetSearchItemFromDb(unSignedWord);
                if (searchItem == null) continue;
                var docsNames = GetDocsName(searchItem);
                tempResult = tempResult.Intersect(docsNames).ToList();
            }

            return tempResult;
        
        }

        private List<string> GetDocsName(ISearchResponse<Doc> searchItem)
        {
            var list = searchItem.Documents.Select(doc => doc.Id).ToList();
            return list;
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