using System.Collections.Generic;
using System.Linq;

namespace InvertedIndexLibrary
{
    public class SearchController : ISearchController
    {
        private IPartitioner _partitioner;
        private IListCalculator _listCalculator;
        private IListOperator _listOperator;
        private IFileNamesExtractor _fileNamesExtractor;
        private ITokenizer _tokenizer;
        private ITokenizeController _tokenizeController;
        private IHashTableCreator _hashTableCreator;
        private IIndexController _indexController;
        private List<string> _unsignedWords;
        private List<string> _plusignedWords;
        private List<string> _minussignedWords;

        public SearchController()
        {
            _partitioner = new Partitioner();
            _listCalculator = new ListCalculator();
            _listOperator = new ListOperator(_listCalculator);
            _fileNamesExtractor = new FileNamesExtractor();
            _tokenizeController = new TokenizeController(_fileNamesExtractor, _tokenizer);
            _tokenizeController = new TokenizeController(_fileNamesExtractor, _tokenizer);
            _hashTableCreator = new HashTableCreator(_tokenizeController);
            _indexController = new IndexController(_hashTableCreator);
            _unsignedWords = new List<string>();
            _minussignedWords = new List<string>();
            _plusignedWords = new List<string>();
        }

        public List<string> SearchDocs(string input)
        {
            List<string> docsSearchingResultSet = new List<string>();
            _partitioner.PartitionInputs(input, _plusignedWords, _minussignedWords,
                _unsignedWords);
            Dictionary<string, List<string>> tableOfWordsAsKeyAndContainingDocsAsValue = _indexController.GetInvertedIndexTable();
            docsSearchingResultSet =
                _listOperator.InitializeResultSetByFirstUnsignedInputWordDocs(_unsignedWords.ElementAt(0),
                    tableOfWordsAsKeyAndContainingDocsAsValue);
            docsSearchingResultSet = _listOperator.GetIntersectedUnsignedWordsContainingDocs(_unsignedWords,
                docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            docsSearchingResultSet = _listOperator.GetDocsWithoutPlusWords(_plusignedWords,
                docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            docsSearchingResultSet = _listOperator.GetRemovedDocsExcludingMinusSignedWords(_minussignedWords,
                docsSearchingResultSet, tableOfWordsAsKeyAndContainingDocsAsValue);
            return docsSearchingResultSet;
        }
    }
}