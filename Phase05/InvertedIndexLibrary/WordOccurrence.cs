
using System;

namespace InvertedIndexLibrary
{
    public class WordOccurrence : IWordOccurence
    {
        public string Term { get; }

        public string Doc { get;  }

        public WordOccurrence(string term, string doc)
        {
            Term = term;
            Doc = doc;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is WordOccurrence))
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }
            return Term != null && Doc != null && Term.Equals(((WordOccurrence) obj).Term) && Doc.Equals(((WordOccurrence) obj).Doc);
        }

        protected bool Equals(WordOccurrence other)
        {
            return Term == other.Term && Doc == other.Doc;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Term, Doc);
        }
    }
}