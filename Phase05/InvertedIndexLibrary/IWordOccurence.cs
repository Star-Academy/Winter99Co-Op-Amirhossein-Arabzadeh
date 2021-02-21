using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public interface IWordOccurence
    {
        public static List<IWordOccurence> Tokens { get; set; }

        public string Term { get; set; }
        public string Doc { get; set; }
    }
}