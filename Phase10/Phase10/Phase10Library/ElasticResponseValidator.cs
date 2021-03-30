using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;

namespace Phase10Library
{
    public class ElasticResponseValidator
    {
        private const string LogfileTxt = "logfile.txt";

        private Dictionary<PipelineFailure, string> _failureReasonsAndMessages;
        
        public ElasticResponseValidator()
        {
            CreateDictionaryOfFailuresAndMessages();
        }

        private void CreateDictionaryOfFailuresAndMessages()
        {
            var pipelineFailuresException = GetIPipelineFailureExceptions();
            _failureReasonsAndMessages = new Dictionary<PipelineFailure, string>();
            foreach (var IPipelineFailureException in pipelineFailuresException)
            {
                var pipelineFailureException = Activator.CreateInstance(IPipelineFailureException);
                _failureReasonsAndMessages.Add(((IPipelineFailureException)pipelineFailureException).PipelineFailure,
                    ((IPipelineFailureException)pipelineFailureException).Message);
            }
        }

        private static IEnumerable<Type> GetIPipelineFailureExceptions()
        {
            var pipelineFailuresException = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(IPipelineFailureException).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .Where(type => !type.IsPublic)
                .ToList();
            return pipelineFailuresException;
        }

        public void Validate(IElasticsearchResponse response)
        {
            if (response is not ElasticsearchResponse<Doc>)
            {
                return;
            }

            var elasticsearchResponse = (ElasticsearchResponse<Doc>) response;
            if (elasticsearchResponse.Success)
            {
                LogException("Response is alright but we should check if all shards are checked.");
                return;
            }

            if (elasticsearchResponse.OriginalException is not ElasticsearchClientException exception) return;
            LogException(_failureReasonsAndMessages[exception.FailureReason.Value]);
        }
        
        private void LogException(string exceptionMessage)
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

            log.WriteLine("Exception happened in ElasticResponseValidator.Validate: " + exceptionMessage);
            log.Close();
        }
    }
}