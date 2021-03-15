using System.Collections.Generic;

namespace Phase10LibraryTest
{
    public class SampleDataProvider
    {
        public Dictionary<string, List<string>> Table { get; set; }
        public List<string> Partition { get; set; }
        public ISet<string> ExpectedSet { get; set; }
        
        private static SampleDataProvider _instance;

        public static SampleDataProvider GetInstance()
        {  
            return _instance ??= new SampleDataProvider();
        }
        
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
            Table = new Dictionary<string, List<string>>
            {
                ["ali"] = new List<string> {"1", "2", "3"},
                ["reza"] = new List<string> {"1"},
                ["javad"] = new List<string> {"1", "10", "35"},
                ["hossein"] = new List<string> {"35"}
            };
        }
    }
}