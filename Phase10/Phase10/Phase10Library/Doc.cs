using System;
using System.IO;

namespace Phase10Library
{
    public class Doc
    {
        private const string LogfileTxt = "logfile.txt";

        public string Name { get;}
        public string Content { get;}
        
        public Doc(string name, string content)
        {
            Validate(name, content);
            Name = name;
            Content = content;
        }

        private void Validate(string name, string content)
        {
            ValidateId(name);
            ValidateContent(content);
        }

        private void ValidateContent(string content)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    throw new ArgumentException("Doc content is either null or white space or id is negative:\n"); 
                }
            }
            catch (ArgumentException exception)
            {
                LogException(exception);

                throw new ArgumentException("Provide content is wither null or empty");
            }
        }

        private void LogException(ArgumentException exception)
        {
            StreamWriter log;
            
            if (!File.Exists(LogfileTxt))
            {
                log = new StreamWriter(LogfileTxt);
            }
            else
            {
                log = File.AppendText(LogfileTxt);
            }

            log.WriteLine("Exception happened in Doc.TryValidateContent: " + exception.Message);
            log.Close();
        }

        private void ValidateId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Provided Id is either null or whiteSpace");
            }
        }
    }
}
