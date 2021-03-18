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
            if (exception.FailureReason == PipelineFailure.Unexpected)
            {
                Console.WriteLine("Unexpected exception is occured.");
            }

            if (exception.FailureReason == PipelineFailure.BadAuthentication)
            {
                Console.WriteLine("Bad authentication is happened.");
            }

            if (exception.FailureReason == PipelineFailure.BadRequest)
            {
                Console.WriteLine("You've requested bad query.");
            }

            if (exception.FailureReason == PipelineFailure.BadResponse)
            {
                Console.WriteLine("The format of response is wrong.");   
            }

            if (exception.FailureReason == PipelineFailure.PingFailure)
            {
                Console.WriteLine("Connection is poor.");
            }

            if (exception.FailureReason == PipelineFailure.SniffFailure)
            {
                Console.WriteLine("");
            }

            if (exception.FailureReason == PipelineFailure.MaxRetriesReached)
            {
                Console.WriteLine("Maximum number of retryings of sending request is used.");
            }

            if (exception.FailureReason == PipelineFailure.MaxTimeoutReached)
            {
                Console.WriteLine("Maximum time of trying is reached.");
            }

            if (exception.FailureReason == PipelineFailure.NoNodesAttempted)
            {
                Console.WriteLine("No node is used to proccess the query");
            }

            if (exception.FailureReason == PipelineFailure.CouldNotStartSniffOnStartup)
            {
                Console.WriteLine("Could not start sniffing on startup");
            }
            if (exception.FailureReason is ArgumentException)
            {
                Console.WriteLine("Some arguments in request to elasticsearch is wrong.");
            }

            if (exception.FailureReason is ArithmeticException)
            {
                Console.WriteLine("Some arithmetic exception is occured during sending request to elasticsearch.");
            }

            if (exception.FailureReason is FormatException)
            {
                Console.WriteLine("Some format exception is occured.");
            }

            if (exception.FailureReason is OverflowException)
            {
                Console.WriteLine("Some overflow exception is occured.");
            }

            if (exception.FailureReason is PipelineException)
            {
                Console.WriteLine("Some pipline exception is occured.");
            }

            if (exception.FailureReason is SystemException)
            {
                Console.WriteLine("Some system exception is occured.");
            }

            if (exception.FailureReason is TimeoutException)
            {
                Console.WriteLine("Timeout exception is occured.");
            }

            if (exception.FailureReason is AccessViolationException)
            {
                Console.WriteLine("Access exception has been occured.");
            }

            if (exception.FailureReason is ArgumentNullException)
            {
                Console.WriteLine("At least one of the arguments was null.");
            }

            if (exception.FailureReason is InvalidCastException)
            {
                Console.WriteLine("invalid cast exption");
            }
        }
    }
}