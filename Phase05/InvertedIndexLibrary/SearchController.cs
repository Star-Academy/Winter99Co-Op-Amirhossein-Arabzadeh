using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class SearchController : ISearchController
    {
        private readonly IPartitioner _partitioner;
        private readonly IListOperator _listOperator;
        private readonly IIndexController _indexController;
        private List<string> _unsignedWords;
        private List<string> _plusSignedWords;
        private List<string> _minusSignedWords;

        public SearchController(IIndexController indexController)
        {
            _partitioner = new Partitioner();
            IListCalculator listCalculator = new ListCalculator();
            _listOperator = new ListOperator(listCalculator);
            _indexController = indexController;
            _unsignedWords = new List<string>();
            _minusSignedWords = new List<string>();
            _plusSignedWords = new List<string>();
        }

        public IEnumerable<string> SearchDocs(string input)
        {
            PartitionInputWords(input);
            List<string> docsSearchingResultSet = new List<string>();
            var tableOfWordsAsKeyAndContainingDocsAsValue = _indexController.GetInvertedIndexTable();
            docsSearchingResultSet = GetIntersectedUnsignedWordsDocsSet(docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);

            docsSearchingResultSet = GetIntersectedResultSetWithAllPlusSignedWordsDocs(docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);

            docsSearchingResultSet = GetResultSetWithoutMinusSignedWords(docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            
            return docsSearchingResultSet;
        }

        private List<string> GetResultSetWithoutMinusSignedWords(List<string> docsSearchingResultSet,
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue)
        {
            if (IsResultSetAndMinusSignedWordsNotEmpty(docsSearchingResultSet))
            {
                docsSearchingResultSet = _listOperator.GetDocsExcludingMinusSignedWords(_minusSignedWords,
                    docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            }

            return docsSearchingResultSet;
        }

        private bool IsResultSetAndMinusSignedWordsNotEmpty(List<string> docsSearchingResultSet)
        {
            return _minusSignedWords.Count > 0 && docsSearchingResultSet.Count > 0;
        }

        private List<string> GetIntersectedResultSetWithAllPlusSignedWordsDocs(List<string> docsSearchingResultSet,
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue)
        {
            if (IsResultSetAndPlusSignedWordsNotEmpty(docsSearchingResultSet))
            {
                docsSearchingResultSet = _listOperator.GetDocsWithoutPlusWords(_plusSignedWords,
                    docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            }

            return docsSearchingResultSet;
        }

        private bool IsResultSetAndPlusSignedWordsNotEmpty(List<string> docsSearchingResultSet)
        {
            return _plusSignedWords.Count > 0 && docsSearchingResultSet.Count > 0;
        }

        private List<string> GetIntersectedUnsignedWordsDocsSet(List<string> docsSearchingResultSet,
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue)
        {
            if (_unsignedWords.Count <= 0)
            {
                return docsSearchingResultSet;
            }

            docsSearchingResultSet =
                _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(_unsignedWords.ElementAt(0),
                    tableOfWordsAsKeyAndContainingDocsAsValue);
            if (docsSearchingResultSet.Count > 0)
            {
                docsSearchingResultSet = _listOperator.GetIntersectedUnsignedWordsContainingDocs(_unsignedWords,
                    docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            }
            
            return docsSearchingResultSet;
        }

        private void PartitionInputWords(string input)
        {
            _unsignedWords = _partitioner.GetUnSignedWords(input);
            _minusSignedWords = _partitioner.GetWantedSignedWords(input, "-");
            _plusSignedWords = _partitioner.GetWantedSignedWords(input, "+");
        }
    }
}