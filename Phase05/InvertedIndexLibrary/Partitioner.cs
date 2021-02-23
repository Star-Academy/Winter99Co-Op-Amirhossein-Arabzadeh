using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace InvertedIndexLibrary
{
    public class Partitioner : IPartitioner
    {
        public List<string> GetWantedSignedWords(string searchingTerm, string sign)
        {
            ValidateInputStrings(searchingTerm, sign);
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where term.StartsWith(sign)
                select term.Substring(1).ToLower());
        }

        public List<string> GetUnSignedWords(string searchingTerm)
        {
            ValidateInputString(searchingTerm);
            List<string> searchingTerms = (Regex.Split(searchingTerm, "\\s")).ToList();
            return new List<string>(from term in searchingTerms
                where !term.StartsWith("+") && !term.StartsWith("-")
                select term.ToLower());
        }

        private void ValidateInputString(string searchingTerm)
        {
            if (IsStringNullOrEmpty(searchingTerm))
            {
                throw new ArgumentException("parameter is null or white space");
            }
        }

        private bool IsStringNullOrEmpty(string searchingTerm)
        {
            return searchingTerm is null || searchingTerm.Trim().Length == 0;
        }

        private void ValidateInputStrings(string searchingTerm, string sign)
        {
            ValidateInputString(searchingTerm);
            ValidateInputString(sign);
        }
    }
}