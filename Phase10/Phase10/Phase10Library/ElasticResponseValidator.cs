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
                Console.WriteLine("Response is alright");
                Console.WriteLine(response.Body);
            }

            // if (response.)
            // {
            //     
            // }
        }
    }
}