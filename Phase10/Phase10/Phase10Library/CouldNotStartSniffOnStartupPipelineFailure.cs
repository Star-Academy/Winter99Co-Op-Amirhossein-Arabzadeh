using Elasticsearch.Net;

namespace Phase10Library
{
    public class CouldNotStartSniffOnStartupPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.CouldNotStartSniffOnStartup;
        public string Message { get; set; } = "Could not start sniffing on startup";
    }
}