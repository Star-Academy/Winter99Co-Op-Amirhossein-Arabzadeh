using System.Collections.Generic;
using InvertedIndexLibrary;
using Xunit;

namespace InvertedIndexTest
{
    public class CalculatorTest
    {
        // ISet<String> createSetOfDifferentModeledInputs(List<String> partition, Dictionary<String, List<String>> table);
        // List<String> minusResultSet(ISet<String> anotherSet, List<String> result);
        // List<String> andResultSet(ISet<String> docs, List<String> result);
        [Fact]
        public void CreateSetOfDifferentPartitions_ShouldReturnSetOfDocsContainingPartitionList_WhenParametersAreValid()
        {
            Dictionary<string, List<string>> table = new Dictionary<string, List<string>>();
            table["ali"] = new List<string>
            {
                "1", "2", "3"
            };
            table["reza"] = new List<string>
            {
                "1"
            };
            table["javad"] = new List<string>
            {
                "1", "10", "35"
            };
            table["hossein"] = new List<string>
            {
                "35"
            };
            var partition = new List<string>
            {
                "ali", "hasan", "hossein"
            };
            var expectedSet = new HashSet<string>
            {
                "1", "2", "3", "35"
            };
            IListCalculator listCalculator = new ListCalculator();
            Assert.Equal(expectedSet, listCalculator.CreateSetOfDifferentPartitions(partition, table));
        }
    }
}