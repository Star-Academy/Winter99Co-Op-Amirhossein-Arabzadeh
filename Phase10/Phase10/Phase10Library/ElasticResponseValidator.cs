using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Phase10Library
{
    public class ElasticResponseValidator
    {
        private Dictionary<PipelineFailure?, string> failureReasonsAndMessages;
        public ElasticResponseValidator()
        {
            CreateDictionaryOfFailuresAndMessages();
        }

        private void CreateDictionaryOfFailuresAndMessages()
        {
            failureReasonsAndMessages = new Dictionary<PipelineFailure?, string>();
            failureReasonsAndMessages.Add(PipelineFailure.Unexpected, "Unexpected exception is occured.");
            failureReasonsAndMessages.Add(PipelineFailure.BadAuthentication, "Bad authentication is happened.");
            failureReasonsAndMessages.Add(PipelineFailure.BadRequest, "You've requested bad query.");
            failureReasonsAndMessages.Add(PipelineFailure.BadResponse, "The format of response is wrong.");
            failureReasonsAndMessages.Add(PipelineFailure.PingFailure, "Connection is poor.");
            failureReasonsAndMessages.Add(PipelineFailure.SniffFailure, "");
            failureReasonsAndMessages.Add(PipelineFailure.MaxRetriesReached, "Maximum number of retryings of sending request is used.");
            failureReasonsAndMessages.Add(PipelineFailure.MaxTimeoutReached, "Maximum time of trying is reached.");
            failureReasonsAndMessages.Add(PipelineFailure.NoNodesAttempted, "No node is used to proccess the query");
            failureReasonsAndMessages.Add(PipelineFailure.CouldNotStartSniffOnStartup, "Could not start sniffing on startup");
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
            Console.WriteLine(failureReasonsAndMessages[exception.FailureReason]);
        }
    }
}