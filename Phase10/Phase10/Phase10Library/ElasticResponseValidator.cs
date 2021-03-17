using System;
using Elasticsearch.Net;

namespace Phase10Library
{
    public class ElasticResponseValidator<T>  where T : class
    {
        //TODO: implement this class
        public void Validate(ElasticsearchResponse<T> response)
        {
            if (response.Success)
            {
                Console.WriteLine("Response is alright but we should check if all shards are checked");
                // if (response.)
                // {
                //     Console.WriteLine(response.Body);
                // }
                return;   
                
            }

            if (response.OriginalException is not ElasticsearchClientException exception) return;
            if (exception.FailureReason is ArgumentException)
            {
                Console.WriteLine("Some arguments in request to elasticsearch is wrong");
            }

            if (exception.FailureReason is ArithmeticException)
            {
                Console.WriteLine("Some arithmetic exception is occured during sending request to elasticsearch");
            }

            if (exception.FailureReason is FormatException)
            {
                Console.WriteLine("Some format exception is occured");
            }

            if (exception.FailureReason is OverflowException)
            {
                Console.WriteLine("Some overflow exception is occured");
            }

            if (exception.FailureReason is PipelineException)
            {
                Console.WriteLine("Some pipline exception is occured");
            }

            if (exception.FailureReason is SystemException)
            {
                Console.WriteLine("Some system exception is occured");
            }

        }
    }
}