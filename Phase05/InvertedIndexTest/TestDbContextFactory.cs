using System;
using System.Collections.Generic;
using InvertedIndexLibrary;
using Microsoft.EntityFrameworkCore;

namespace InvertedIndexTest
{
    public class TestDbContextFactory
    {
        
        public InvertedIndexContext Seed()
        {
            var option = new DbContextOptionsBuilder<InvertedIndexContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var invertedIndexContext = new InvertedIndexContext(option);
            invertedIndexContext.Database.EnsureCreated();
            var doc1 = new Doc(1.ToString());
            var doc2 = new Doc(2.ToString());
            var doc3 = new Doc(3.ToString());
            var searchItems = new List<SearchItem>
            {
                new SearchItem
                {
                    Term = "ali",
                    Docs = new List<Doc>
                    {
                        doc1,
                        doc2,
                        doc3,
                    }
                },
                new SearchItem
                {
                    Term = "hasan",
                    Docs = new List<Doc>
                    {
                        doc1
                    },
                },
                new SearchItem
                {
                    Term = "hossein",
                    Docs = new List<Doc>
                    {
                        doc2,
                        doc3
                    },
                },
                new SearchItem
                {
                    Term = "reza",
                    Docs = new List<Doc>
                    {
                        doc2,
                    },
                },
            };
            invertedIndexContext.SearchingItems.AddRange(searchItems);
            invertedIndexContext.SaveChanges();

            return invertedIndexContext;
        }

    }
}