using System;
using Phase10Library;

namespace ImportRun
{
    class Program
    {
        static void Main(string[] args)
        {
            var indexDefiner = new IndexDefiner();
            indexDefiner.CreateIndex(Indexes.DocsIndex);
        }
    }
}