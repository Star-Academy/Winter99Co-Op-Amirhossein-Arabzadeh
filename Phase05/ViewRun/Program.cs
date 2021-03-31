﻿using System;
using InvertedIndexLibrary;
using Microsoft.EntityFrameworkCore;

namespace ViewRun
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IFileNamesExtractor fileNamesExtractor = new FileNamesExtractor();
            ITokenizer tokenizer = new Tokenizer();
            ITokenizeController tokenizeController = new TokenizeController(fileNamesExtractor, tokenizer);
            IHashTableCreator hashTableCreator = new HashTableCreator(tokenizeController);
            var option = new
                    DbContextOptionsBuilder<InvertedIndexContext>().
                UseSqlServer(@"Server=DESKTOP-PT8A28F;Database=InvertedIndexDb;Trusted_Connection=True;").Options;

            using var invertedIndexContext = new InvertedIndexContext(option);
            IIndexController indexController = new IndexController(hashTableCreator, invertedIndexContext);
            IView view = new View();
            view.Run(indexController, invertedIndexContext);
        }
    }
}