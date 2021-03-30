using Elasticsearch.Net;

namespace Phase10Library
{
    public class MaxTimeoutReachedPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.MaxTimeoutReached;
        public string Message { get; set; } = "Maximum time of trying is reached.";
    }
}