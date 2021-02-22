﻿using System;
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
            IIndexController indexController = new IndexController(hashTableCreator);
            indexController.ProcessDocs("../../../../Resources/BigEnglishData");
            IView view = new View();
            view.run();
        }
    }
}