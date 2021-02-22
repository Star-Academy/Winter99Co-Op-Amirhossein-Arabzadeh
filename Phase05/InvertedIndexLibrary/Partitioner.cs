using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace InvertedIndexLibrary
{
    public class Partitioner : IPartitioner
    {
        public void PartitionInputs(string searchingTerm, List<string> plusSignedInputWords, List<string> minusSignedInputWords,
            List<string> unSignedInputWords)
        {
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            plusSignedInputWords = new List<string>(from term in searchingTerms
                where term.StartsWith("+")
                select term.Substring(1).ToLower());
            minusSignedInputWords = new List<string>(from term in searchingTerms
                where term.StartsWith("-")
                select term.Substring(1).ToLower());
            unSignedInputWords = new List<string>(from term in searchingTerms
                where !plusSignedInputWords.Contains("+" + term) && !minusSignedInputWords.Contains("-" + term)
                select term);
        }
    }
}