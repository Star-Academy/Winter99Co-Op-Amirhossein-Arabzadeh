using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace InvertedIndexLibrary
{
    public class Partitioner : IPartitioner
    {
        public List<string> GetWantedSignedWords(string searchingTerm, string sign)
        {
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where term.StartsWith(sign)
                select term.Substring(1).ToLower());
        }
        public List<string> GetUnSignedWords(string searchingTerm)
        {
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where !term.StartsWith("+") && !term.StartsWith("-")
                select term.ToLower());
        }
    }
}