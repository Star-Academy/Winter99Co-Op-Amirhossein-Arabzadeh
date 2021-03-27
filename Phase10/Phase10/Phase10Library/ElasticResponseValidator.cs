using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Phase10Library
{
    public class ElasticResponseValidator
    {
        private Dictionary<PipelineFailure?, string> _failureReasonsAndMessages;
        public ElasticResponseValidator()
        {
            CreateDictionaryOfFailuresAndMessages();
        }

        private void CreateDictionaryOfFailuresAndMessages()
        {
            _failureReasonsAndMessages = new Dictionary<PipelineFailure?, string>();
            _failureReasonsAndMessages.Add(PipelineFailure.Unexpected, "Unexpected exception is occured.");
            _failureReasonsAndMessages.Add(PipelineFailure.BadAuthentication, "Bad authentication is happened.");
            _failureReasonsAndMessages.Add(PipelineFailure.BadRequest, "You've requested bad query.");
            _failureReasonsAndMessages.Add(PipelineFailure.BadResponse, "The format of response is wrong.");
            _failureReasonsAndMessages.Add(PipelineFailure.PingFailure, "Connection is poor.");
            _failureReasonsAndMessages.Add(PipelineFailure.SniffFailure, "");
            _failureReasonsAndMessages.Add(PipelineFailure.MaxRetriesReached, "Maximum number of retryings of sending request is used.");
            _failureReasonsAndMessages.Add(PipelineFailure.MaxTimeoutReached, "Maximum time of trying is reached.");
            _failureReasonsAndMessages.Add(PipelineFailure.NoNodesAttempted, "No node is used to proccess the query");
            _failureReasonsAndMessages.Add(PipelineFailure.CouldNotStartSniffOnStartup, "Could not start sniffing on startup");
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
                Console.WriteLine("Response is alright but we should check if all shards are checked.");
                return;   
                
            }

            if (elasticsearchResponse.OriginalException is not ElasticsearchClientException exception) return;
            Console.WriteLine(_failureReasonsAndMessages[exception.FailureReason]);
        }
    }
}