using System;
using Nest;

namespace Phase10Library
{
    public class ElasticClientFactory 
    { 
        private IElasticClient CreateInitialClient(string url)
        {
            var uri = new Uri(url);
            var connectionSettings = CreateConnectionSettings(uri);
            return new ElasticClient(connectionSettings);
        }

        private ConnectionSettings CreateConnectionSettings(Uri uri)
        {
            var connectionSettings = new ConnectionSettings(uri);
            connectionSettings.EnableDebugMode();
            return connectionSettings;
        }

        public IElasticClient CreateElasticClient(string url)
        {
            var client = CreateInitialClient(url);
            return client;
        }
    }
}