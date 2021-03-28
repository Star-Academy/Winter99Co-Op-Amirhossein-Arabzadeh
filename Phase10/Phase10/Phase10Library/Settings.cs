using System.Text.Json;

namespace Phase10Library
{
    public class Settings
    {
        public Addresses Addresses { get; set; }
        public Analyzers Analyzers { get; set; }
        public Indexes Indexes { get; set; }
        public KeyWords KeyWords { get; set; }
        public TokenFilters TokenFilters { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
    
    public class Addresses
    {
        public string FolderRelativePath { get; set; }
        public string HttpLocalhost { get; set; }
    }

    public class Analyzers
    {
        public string NgramAnalyzer { get; set; }
    }

    public class Indexes
    {
        public string DocsIndex { get; set; }
    }

    public class KeyWords
    {
        public string KeyWord { get; set; }
        public string Standard { get; set; }
        public string MaxNgramDiff { get; set; }
    }

    public class TokenFilters
    {
        public string NgramFilter { get; set; }
        public string EnglishStopWords { get; set; }
        public string WordDelimiter { get; set; }
        public string LowerCase { get; set; }
    }
}