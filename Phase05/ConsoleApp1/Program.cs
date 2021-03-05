using InvertedIndexLibrary;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileNamesExtractor fileNamesExtractor = new FileNamesExtractor();
            ITokenizer tokenizer = new Tokenizer();
            ITokenizeController tokenizeController = new TokenizeController(fileNamesExtractor, tokenizer);
            IHashTableCreator hashTableCreator = new HashTableCreator(tokenizeController);
            using (var invertedIndexContext = new InvertedIndexContext())
            {
                IIndexController indexController = new IndexController(hashTableCreator, invertedIndexContext);
                // indexController.ProcessDocs("../../../../Resources/SmallEnglishData");
                IView view = new View();
                view.Run(indexController);
            }
        }
    }
}