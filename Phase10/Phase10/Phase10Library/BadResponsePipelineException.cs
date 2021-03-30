using Elasticsearch.Net;

namespace Phase10Library
{
    public class BadResponsePipelineException : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.BadResponse;
        public string Message { get; set; } = "The format of response is wrong.";
    }
}