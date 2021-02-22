using System.Collections.Generic;

namespace InvertedIndexTest
{
    public class SampleDataProvider
    {
        public Dictionary<string, List<string>> Table { get; set; }
        public List<string> Partition { get; set; }
        public ISet<string> ExpectedSet { get; set; }
        
        private static SampleDataProvider _instance;

        public static SampleDataProvider GetInstance()
        {
            if (_instance is null)
            {
                _instance = new SampleDataProvider();
            }

            return _instance;
        }
        

        public IEnumerable<object[]> InvalidListAndDictionaryArguments { get; set; } 

        public static IEnumerable<object[]> InvalidSetAndListArguments { get; set; } 

        private SampleDataProvider()
        {
            CreateSampleTable();
            CreateSamplePartition();
            CreateSampleSet();
        }

        
        private void CreateSampleSet()
        {
            ExpectedSet = new HashSet<string>
            {
                "1", "2", "3", "35"
            };
        }

        private void CreateSamplePartition()
        {
            Partition = new List<string>
            {
                "ali", "hasan", "hossein"
            };
        }

        private void CreateSampleTable()
        {
            Table = new Dictionary<string, List<string>>();
            Table["ali"] = new List<string>
            {
                "1", "2", "3"
            };
            Table["reza"] = new List<string>
            {
                "1"
            };
            Table["javad"] = new List<string>
            {
                "1", "10", "35"
            };
            Table["hossein"] = new List<string>
            {
                "35"
            };
        }
    }
}