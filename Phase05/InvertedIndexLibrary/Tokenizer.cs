using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace InvertedIndexLibrary
{
    public class Tokenizer : ITokenizer
    {
        public List<IWordOccurence> TokenizeFiles(IEnumerable<string> filePaths)
        {
            var tokens = new List<IWordOccurence>(); 
            foreach (var filePath in filePaths)
            {
                tokens.AddRange(TokenizeFile(filePath));
            }

            return tokens;
        }

        private List<IWordOccurence> TokenizeFile(string filePath)
        {
            string[] lines = null;
            lines = ValidateExistanceOfFile(filePath);
            
            var tokens = new List<IWordOccurence>();
            foreach (var line in lines)
            {
                tokens.AddRange(TokenizeLine(line, Path.GetFileName(filePath)));
            }

            return tokens;
        }

        private static string[] ValidateExistanceOfFile(string filePath)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                throw new FileNotFoundException("filePath is invalid");
            }

            return lines;
        }

        private List<IWordOccurence> TokenizeLine(string line, string filePath)
        {
            var tokens = new List<IWordOccurence>();
            var terms = Regex.Split(line, @"\s");
            foreach (var term in terms)
            {
                var token = new WordOccurrence(term.ToLower(), Path.GetFileName(filePath));
                tokens.Add(token);
            }

            return tokens;
        }
    }
}