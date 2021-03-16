using System;
using System.Linq.Expressions;

namespace Phase10Library
{
    public class Doc
    {
        public Doc(string name, string content)
        {
            ValidateId(name);
            try
            {
                ValidateContent(content);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            Name = name;
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

        public string Name { get; set; }
        public string Content { get; set; }
        
    }
}
