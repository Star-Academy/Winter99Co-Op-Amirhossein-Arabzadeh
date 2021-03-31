using Nest;

namespace Phase10Library
{
    public interface IElasticClientFactory
    {
        IElasticClient CreateElasticClient(string url);
    }
}