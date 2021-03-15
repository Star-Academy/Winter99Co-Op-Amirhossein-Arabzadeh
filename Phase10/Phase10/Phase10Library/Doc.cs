using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndexLibrary
{
    public class Doc
    {
        public Doc(int id, string content)
        {
            ValidateId(id);
            ValidateContent(content);
            
            Id = id;
            Content = content;
        }

        private void ValidateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException();
            }
        }

        private void ValidateId(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException();
            }
        }

        public int Id { get; set; }
        public string Content { get; set; }
        
    }
}
