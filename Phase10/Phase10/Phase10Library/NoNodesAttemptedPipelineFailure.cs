using Elasticsearch.Net;

namespace Phase10Library
{
    public class NoNodesAttemptedPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.NoNodesAttempted;
        public string Message { get; set; } = "No node is used to proccess the query";
    }
}