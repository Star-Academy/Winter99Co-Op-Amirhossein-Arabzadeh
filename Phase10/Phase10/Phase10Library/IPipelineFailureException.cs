using Elasticsearch.Net;

namespace Phase10Library
{
    internal interface IPipelineFailureException
    {
        public PipelineFailure PipelineFailure { get; set; }
        public string Message { get; set; }
    }
}