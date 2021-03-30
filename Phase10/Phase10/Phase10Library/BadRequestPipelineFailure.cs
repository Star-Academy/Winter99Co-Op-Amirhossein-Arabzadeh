using Elasticsearch.Net;

namespace Phase10Library
{
    public class BadRequestPipelineFailure : IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; } = PipelineFailure.BadRequest;
        public string Message { get; set; } = "You've requested bad query.";
    }
}