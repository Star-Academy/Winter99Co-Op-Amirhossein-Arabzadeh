using System;
using Nest;

namespace Phase10Library
{
    public class ElasticClientFactory 
    {
        private IElasticClient _client;

        private IElasticClient CreateInitialClient(string url)
        {
            var uri = new Uri (url);
            var connectionSettings = new ConnectionSettings (uri);
            // DebugMode gives you the request in each request to make debuging easier
            // But don't forget to only use it in debugging, because its usage has some overhead
            // and should not be used in production
            connectionSettings.EnableDebugMode();
            return new ElasticClient (connectionSettings);
        }

        public IElasticClient CreateElasticClient(string url)
        {
            var client = CreateInitialClient(url);
            return client;
        }
    }
}