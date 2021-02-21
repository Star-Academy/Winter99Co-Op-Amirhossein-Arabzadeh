#nullable enable
using System;
using System.Collections.Generic;

namespace InvertedIndexLibrary
{
    public class WordOccurence : IWordOccurence
    {
        public static List<IWordOccurence> Tokens { get; set; }

        public WordOccurence(string term, string doc)
        {
            Tokens = new List<IWordOccurence>();
            Term = term;
            Doc = doc;
        }

        public string? Term { get; set; }
        public string? Doc { get; set; }
        public override bool Equals(object? obj)
        {
            if (!(obj is WordOccurence))
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }
            return Term != null && Doc != null && Term.Equals(((WordOccurence) obj).Term) && Doc.Equals(((WordOccurence) obj).Doc);
        }
    }
}