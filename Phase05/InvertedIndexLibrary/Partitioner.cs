using System;
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
            if (searchingTerm is null || sign is null || searchingTerm.Trim().Length == 0 || sign.Trim().Length == 0)
            {
                throw new ArgumentException("parameters are white space or null");
            }
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where term.StartsWith(sign)
                select term.Substring(1).ToLower());
        }
        public List<string> GetUnSignedWords(string searchingTerm)
        {
            if (searchingTerm is null || searchingTerm.Trim().Length == 0)
            {
                throw new ArgumentException("parameter is null or white space");
            }
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where !term.StartsWith("+") && !term.StartsWith("-")
                select term.ToLower());
        }
    }
}