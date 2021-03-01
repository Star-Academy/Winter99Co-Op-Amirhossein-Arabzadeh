using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace InvertedIndexLibrary
{
    public class Tokenizer : ITokenizer
    {
        public List<WordOccurrence> TokenizeFiles(IEnumerable<string> filePaths)
        {
            var tokens = new List<WordOccurrence>(); 
            foreach (var filePath in filePaths)
            {
                tokens.AddRange(TokenizeFile(filePath));
            }

            return tokens;
        }

        private IEnumerable<WordOccurrence> TokenizeFile(string filePath)
        {
            var lines = ValidateExistenceOfFile(filePath);
            
            var tokens = new List<WordOccurrence>();
            foreach (var line in lines)
            {
                tokens.AddRange(TokenizeLine(line, Path.GetFileName(filePath)));
            }

            return tokens;
        }

        private static string[] ValidateExistenceOfFile(string filePath)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception)
            {
                throw new FileNotFoundException("filePath is invalid");
            }

            return lines;
        }

        private IEnumerable<WordOccurrence> TokenizeLine(string line, string filePath)
        {
            var terms = Regex.Split(line, @"\s");

            return terms.Select(term => new WordOccurrence(term.ToLower(), Path.GetFileName(filePath))).ToList();
        }
    }
}