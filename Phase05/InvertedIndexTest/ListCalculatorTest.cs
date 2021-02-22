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
        private static Dictionary<string, List<string>> _table;
        private static List<string> _partition;
        private static ISet<string> _expectedSet;
        private static IListCalculator _listCalculator;

        public ListCalculatorTest()
        {
            _listCalculator = new ListCalculator();
            CreateSampleTable();
            CreateSamplePartition();
            CreateSampleSet();
        }

        private void CreateSampleSet()
        {
            _expectedSet = new HashSet<string>
            {
                "1", "2", "3", "35"
            };
        }

        private static void CreateSamplePartition()
        {
            _partition = new List<string>
            {
                "ali", "hasan", "hossein"
            };
        }

        private void CreateSampleTable()
        {
            _table = new Dictionary<string, List<string>>();
            _table["ali"] = new List<string>
            {
                "1", "2", "3"
            };
            _table["reza"] = new List<string>
            {
                "1"
            };
            _table["javad"] = new List<string>
            {
                "1", "10", "35"
            };
            _table["hossein"] = new List<string>
            {
                "35"
            };
        }

        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            Assert.Equal(_expectedSet, _listCalculator.CreateSetOfDifferentPartitions(_partition, _table));
        }

        public static IEnumerable<object[]> InvalidCreateSetOfDifferentPartitionsArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new List<string>(), null},
            new object[] {new List<string>(), _table},
            new object[] {_partition, null},
            new object[] {_partition, new Dictionary<string, List<string>>()},
        };

        [Theory]
        [MemberData(nameof(InvalidCreateSetOfDifferentPartitionsArguments))]
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
        public static IEnumerable<object[]> InvalidSetAndListArguments = new List<object[]>
        {
            new object[] {null, null},
            new object[] {new HashSet<string>(), null},
            new object[] {new HashSet<string>(), _partition},
            new object[] {_expectedSet, null},
            new object[] {_expectedSet, new List<string>()},
        };
        
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

    }
}