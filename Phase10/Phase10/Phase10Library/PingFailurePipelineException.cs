using Elasticsearch.Net;

namespace Phase10Library
{
    public class PingFailurePipelineException : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.PingFailure;
        public string Message { get; set; } = "Connection is poor.";
    }
}