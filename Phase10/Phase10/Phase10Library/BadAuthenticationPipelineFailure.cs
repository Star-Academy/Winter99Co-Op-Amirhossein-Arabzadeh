using Elasticsearch.Net;

namespace Phase10Library
{
    public class BadAuthenticationPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.BadAuthentication;
        public string Message { get; set; } = "Bad authentication is happened.";
    }
}