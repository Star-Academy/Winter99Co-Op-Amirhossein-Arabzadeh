using System;
using Elasticsearch.Net;

namespace Phase10Library
{
    public class ElasticResponseValidator<T>  where T : class
    {
        public void Validate(ElasticsearchResponse<T> response)
        {
            if (response.Success)
            {
                Console.WriteLine("Response is alright but we should check if all shards are checked.");
                return;   
                
            }

            if (response.OriginalException is not ElasticsearchClientException exception) return;
            switch (exception.FailureReason)
            {
                case PipelineFailure.Unexpected:
                    Console.WriteLine("Unexpected exception is occured.");
                    break;
                case PipelineFailure.BadAuthentication:
                    Console.WriteLine("Bad authentication is happened.");
                    break;
                case PipelineFailure.BadRequest:
                    Console.WriteLine("You've requested bad query.");
                    break;
                case PipelineFailure.BadResponse:
                    Console.WriteLine("The format of response is wrong.");
                    break;
                case PipelineFailure.PingFailure:
                    Console.WriteLine("Connection is poor.");
                    break;
                case PipelineFailure.SniffFailure:
                    Console.WriteLine("");
                    break;
                case PipelineFailure.MaxRetriesReached:
                    Console.WriteLine("Maximum number of retryings of sending request is used.");
                    break;
                case PipelineFailure.MaxTimeoutReached:
                    Console.WriteLine("Maximum time of trying is reached.");
                    break;
                case PipelineFailure.NoNodesAttempted:
                    Console.WriteLine("No node is used to proccess the query");
                    break;
                case PipelineFailure.CouldNotStartSniffOnStartup:
                    Console.WriteLine("Could not start sniffing on startup");
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}