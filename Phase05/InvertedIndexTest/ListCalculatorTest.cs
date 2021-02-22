using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InvertedIndexLibrary;
using Xunit;

namespace InvertedIndexTest
{
    public class ListCalculatorTest
    {
        private static IListCalculator _listCalculator;
        
        private static SampleDataProvider _sampleDataProvider = SampleDataProvider.GetInstance();

        public ListCalculatorTest()
        {
            _listCalculator = new ListCalculator();
        }

        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            Assert.Equal(_sampleDataProvider.ExpectedSet, _listCalculator.CreateSetOfDifferentPartitions(_sampleDataProvider.Partition, _sampleDataProvider.Table));
        }

        [Theory]
        [MemberData(nameof(InvalidListAndDictionaryArguments))]
        public void
            CreateSetOfDifferentPartitions_ShouldThrowArgumentException_WhenParametersAreInvalid(
                List<string> partition, Dictionary<string, List<string>> table)
        {
            Action action =  () => _listCalculator.CreateSetOfDifferentPartitions(partition, table);
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
            List<string> expectedList = new List<string>
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
            List<string> sampleInputList = (from number in  Enumerable.Range(1, 60)
                select number.ToString()).ToList();
            ISet<string> sampleInputSet = (from number in  Enumerable.Range(1, 5)
                select number.ToString()).ToHashSet();
            
            List<string> expectedList = new List<string>
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
            new object[] {new HashSet<string>(), _sampleDataProvider.Partition},
            new object[] {_sampleDataProvider.ExpectedSet, null},
            new object[] {_sampleDataProvider.ExpectedSet, new List<string>()},
        };


        public static IEnumerable<Object[]> InvalidListAndDictionaryArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new List<string>(), null},
            new object[] {new List<string>(), _sampleDataProvider.Table},
            new object[] {_sampleDataProvider.Partition, null},
            new object[] {_sampleDataProvider.Partition, new Dictionary<string, List<string>>()},
        };
    }
}