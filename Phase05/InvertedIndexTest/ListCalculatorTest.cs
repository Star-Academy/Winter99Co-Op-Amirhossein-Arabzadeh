using System;
using System.Collections.Generic;
using System.Linq;
using InvertedIndexLibrary;
using Xunit;

namespace InvertedIndexTest
{
    public class ListCalculatorTest : IDisposable
    {
        private static IListCalculator _listCalculator;
        private static readonly SampleDataProvider SampleDataProvider = SampleDataProvider.GetInstance();
        private readonly InvertedIndexContext _invertedIndexContext;


        public ListCalculatorTest()
        {
            var testDbContextFactory = new TestDbContextFactory();
            _invertedIndexContext = testDbContextFactory.Seed();
            _listCalculator = new ListCalculator(_invertedIndexContext);
            
        }

        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            
            Assert.Equal(new HashSet<string>{"1", "2"}, _listCalculator.GetDocsOfWordsList(new List<string>
            {
                "hasan",
                "reza",
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
            void Action() => _listCalculator.GetDocsOfWordsList(partition);
            Assert.Throws<ArgumentException>(Action);
        }

        [Fact]
        public void MinusElementsOfSetFromList_ShouldReturnInputListWithoutElementOfInputSet_WhenParametersAreValid()
        {
            var sampleInputList = (from number in  Enumerable.Range(1, 60)
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
            void Action() => _listCalculator.MinusElementsOfSetFromList(set, list);
            Assert.Throws<ArgumentException>(Action);
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
            void Action() => _listCalculator.AndListWithSet(set, list);
            Assert.Throws<ArgumentException>(Action);
        }


        public static IEnumerable<object[]> InvalidSetAndListArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new HashSet<string>(), null},
            new object[] {new HashSet<string>(), SampleDataProvider.Partition},
            new object[] {SampleDataProvider.ExpectedSet, null},
            new object[] {SampleDataProvider.ExpectedSet, new List<string>()},
        };


        public static IEnumerable<object[]> InvalidListAndDictionaryArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {new List<string>()},
            new object[] {new List<string>()},
        };
    }
}