using Elasticsearch.Net;

namespace Phase10Library
{
    public class MaxRetriesReachedPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.MaxRetriesReached;
        public string Message { get; set; } = "Maximum number of retryings of sending request is used.";
    }
}