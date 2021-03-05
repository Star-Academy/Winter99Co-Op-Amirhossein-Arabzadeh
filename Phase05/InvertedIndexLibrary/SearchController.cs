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
            InvertedIndexContext invertedIndexContext = null;//new InvertedIndexContext();
            IListCalculator listCalculator = new ListCalculator(invertedIndexContext);
            _listOperator = new ListOperator(listCalculator, invertedIndexContext);
            _indexController = indexController;
            _unsignedWords = new List<string>();
            _minusSignedWords = new List<string>();
            _plusSignedWords = new List<string>();
        }

        public IEnumerable<string> SearchDocs(string input)
        {
            PartitionInputWords(input);
            var docsSearchingResultSet = new List<string>();
            docsSearchingResultSet = GetIntersectedUnsignedWordsDocsSet(docsSearchingResultSet);

            docsSearchingResultSet = GetIntersectedResultSetWithAllPlusSignedWordsDocs(docsSearchingResultSet);

            docsSearchingResultSet = GetResultSetWithoutMinusSignedWords(docsSearchingResultSet);
            
            return docsSearchingResultSet;
        }

        private List<string> GetResultSetWithoutMinusSignedWords(List<string> docsSearchingResultSet)
        {
            if (IsResultSetAndMinusSignedWordsNotEmpty(docsSearchingResultSet))
            {
                docsSearchingResultSet = _listOperator.GetDocsExcludingMinusSignedWords(_minusSignedWords,
                    docsSearchingResultSet);
            }

            return docsSearchingResultSet;
        }

        private bool IsResultSetAndMinusSignedWordsNotEmpty(List<string> docsSearchingResultSet)
        {
            return _minusSignedWords.Count > 0 && docsSearchingResultSet.Count > 0;
        }

        private List<string> GetIntersectedResultSetWithAllPlusSignedWordsDocs(List<string> docsSearchingResultSet)
        {
            if (IsResultSetAndPlusSignedWordsNotEmpty(docsSearchingResultSet))
            {
                docsSearchingResultSet = _listOperator.GetDocsWithoutPlusWords(_plusSignedWords,
                    docsSearchingResultSet);
            }

            return docsSearchingResultSet;
        }

        private bool IsResultSetAndPlusSignedWordsNotEmpty(List<string> docsSearchingResultSet)
        {
            return _plusSignedWords.Count > 0 && docsSearchingResultSet.Count > 0;
        }

        private List<string> GetIntersectedUnsignedWordsDocsSet(List<string> docsSearchingResultSet)
        {
            if (_unsignedWords.Count <= 0)
            {
                return docsSearchingResultSet;
            }

            docsSearchingResultSet =
                _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(_unsignedWords.ElementAt(0));
            if (docsSearchingResultSet.Count > 0)
            {
                docsSearchingResultSet = _listOperator.GetIntersectedUnsignedWordsContainingDocs(_unsignedWords,
                    docsSearchingResultSet);
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