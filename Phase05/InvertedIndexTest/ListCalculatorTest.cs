﻿using System;
using System.Collections.Generic;
using System.Linq;
using InvertedIndexLibrary;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InvertedIndexTest
{
    public class ListCalculatorTest
    {
        private static IListCalculator _listCalculator;
        private static readonly SampleDataProvider SampleDataProvider = SampleDataProvider.GetInstance();
        private readonly InvertedIndexContext _invertedIndexContext;


        private void Seed(InvertedIndexContext invertedIndexContext)
        {
            var doc1 = new Doc(1);
            var doc2 = new Doc(2);
            var doc3 = new Doc(3);
            var searchItems = new List<SearchItem>
            {
                new SearchItem
                {
                    Id = "Ali",
                    Docs = new List<Doc>
                    {
                        doc1,
                        doc2,
                        doc3,
                    }
                },
                new SearchItem
                {
                    Id = "Hasan",
                    Docs = new List<Doc>
                    {
                        doc1
                    },
                },
                new SearchItem
                {
                    Id = "Hossein",
                    Docs = new List<Doc>
                    {
                        doc2,
                        doc3
                    },
                },
                new SearchItem
                {
                    Id = "Reza",
                    Docs = new List<Doc>
                    {
                        doc2,
                    },
                },
            };
            invertedIndexContext.SearchingItems.AddRange(searchItems);
            invertedIndexContext.SaveChanges();
        }

        public ListCalculatorTest()
        {
            var option = new DbContextOptionsBuilder<InvertedIndexContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _invertedIndexContext = new InvertedIndexContext(option);
            _invertedIndexContext.Database.EnsureCreated();
            Seed(_invertedIndexContext);
            
            _listCalculator = new ListCalculator(_invertedIndexContext);
            
            // Dispose();

        }

        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            
            Assert.Equal(new HashSet<string>{"1", "2"}, _listCalculator.GetDocsOfWordsList(new List<string>
            {
                "Hasan",
                "Reza",
            }));
            
            
        }


        public void Dispose()
        {
            _invertedIndexContext.Database.EnsureDeleted();
            _invertedIndexContext.Dispose();
        }


        [Theory]
        [MemberData(nameof(InvalidListAndDictionaryArguments))]
        public void
            CreateSetOfDifferentPartitions_ShouldThrowArgumentException_WhenParametersAreInvalid(
                List<string> partition)
        {
            Action action =  () => _listCalculator.GetDocsOfWordsList(partition);
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void MinusElementsOfSetFromList_ShouldReturnInputListWithoutElementOfInputSet_WhenParametersAreValid()
        {
            List<string> sampleInputList = (from number in  Enumerable.Range(1, 60)
                    select number.ToString()).ToList();
            ISet<string> sampleInputSet = (from number in  Enumerable.Range(1, 60)
                select number.ToString()).ToHashSet();
            sampleInputSet.Remove("1");
            sampleInputSet.Remove("3");
            sampleInputSet.Remove("8");
            sampleInputSet.Remove("58");
            var expectedList = new List<string>
            {
                "1",
                "3",
                "8",
                "58",
            };
            Assert.Equal(expectedList, _listCalculator.MinusElementsOfSetFromList(sampleInputSet, sampleInputList));
        }

        [Theory]
        [MemberData(nameof(InvalidSetAndListArguments))]
        public void
            MinusElementsOfSetFromList_ShouldThrowArgumentException_WhenParametersAreInvalid(
                ISet<string> set, List<string> list)
        {
            Action action = () => _listCalculator.MinusElementsOfSetFromList(set, list);
            Assert.Throws<ArgumentException>(action);
        }
        [Fact]
        public void AndListWithSet_ShouldReturnInputListWithoutElementOfInputSet_WhenParametersAreValid()
        {
            var sampleInputList = (from number in  Enumerable.Range(1, 60)
                select number.ToString()).ToList();
            ISet<string> sampleInputSet = (from number in  Enumerable.Range(1, 5)
                select number.ToString()).ToHashSet();
            
            var expectedList = new List<string>
            {
                "1",
                "2",
                "3",
                "4",
                "5",
            };
            Assert.Equal(expectedList, _listCalculator.AndListWithSet(sampleInputSet, sampleInputList));
        }

        [Theory]
        [MemberData(nameof(InvalidSetAndListArguments))]
        public void
            AndListWithSet_ShouldThrowArgumentException_WhenParametersAreInvalid(
                ISet<string> set, List<string> list)
        {
            Action action = () => _listCalculator.AndListWithSet(set, list);
            Assert.Throws<ArgumentException>(action);
        }


        public static IEnumerable<Object[]> InvalidSetAndListArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new HashSet<string>(), null},
            new object[] {new HashSet<string>(), SampleDataProvider.Partition},
            new object[] {SampleDataProvider.ExpectedSet, null},
            new object[] {SampleDataProvider.ExpectedSet, new List<string>()},
        };


        public static IEnumerable<Object[]> InvalidListAndDictionaryArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {new List<string>()},
            new object[] {new List<string>()},
        };
    }
}