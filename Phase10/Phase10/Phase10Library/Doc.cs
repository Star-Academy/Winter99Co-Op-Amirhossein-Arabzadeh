using System;
using System.Linq.Expressions;

namespace Phase10Library
{
    public class Doc
    {
        public Doc(string id, string content)
        {
            ValidateId(id);
            try
            {
                ValidateContent(content);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
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

        private void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException();
            }
        }

        public string Id { get; set; }
        public string Content { get; set; }
        
    }
}
