using System.Collections.Generic;

namespace Phase10Library
{
    public interface IImporter<T> where T : class
    {
        void Import(IEnumerable<T> documents, string index);
    }
}