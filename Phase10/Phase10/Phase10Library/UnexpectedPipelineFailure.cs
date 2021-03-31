using Elasticsearch.Net;

namespace Phase10Library
{
    public class UnexpectedPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.Unexpected;
        public string Message { get; set; } = "Unexpected exception is occured.";
    }
}