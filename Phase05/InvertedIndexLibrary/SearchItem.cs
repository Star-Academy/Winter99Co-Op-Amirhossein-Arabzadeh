using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexLibrary
{
    public class SearchItem
    {
        public int Id { get; set; }
        public List<Doc> ContainingDocs { get; set; }
    }
}
