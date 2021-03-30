using Elasticsearch.Net;

namespace Phase10Library
{
    public class SniffFailurePipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.SniffFailure;
        public string Message { get; set; } = "Sniff failure has happened";
    }
}