using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexLibrary
{
    public class SearchItem
    {

        public List<Doc> Docs { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string Id{ get; set; }


        public override bool Equals(object obj)
        {
            return obj is SearchItem item &&
                   EqualityComparer<List<Doc>>.Default.Equals(Docs, item.Docs) &&
                   Id.Equals(item.Id);
        }
    }
}
