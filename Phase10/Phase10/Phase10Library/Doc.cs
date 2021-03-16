﻿using System;

namespace Phase10Library
{
    public class Doc
    {
        public Doc(string name, string content)
        {
            Validate(name, content);
            Name = name;
            Content = content;
        }

        private void Validate(string name, string content)
        {
            ValidateId(name);
            TryValidateContent(content);
        }

        private void TryValidateContent(string content)
        {
            try
            {
                ValidateContent(content);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ValidateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Doc content is either null or white space or id is negative:\n"); 
            }
        }

        private void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Provided Id is either null or whiteSpace");
            }
        }

        public string Name { get;}
        public string Content { get;}
        
    }
}
