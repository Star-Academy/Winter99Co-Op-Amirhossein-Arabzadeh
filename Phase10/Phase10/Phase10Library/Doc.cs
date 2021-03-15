using System;

namespace Phase10Library
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
                throw new ArgumentException("Doc content is either null or white space or id is negative");
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
