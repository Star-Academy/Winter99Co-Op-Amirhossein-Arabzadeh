﻿using System;
using System.Linq;
using Nest;

namespace Phase10Library
{
    public class ElasticClientFactory : IElasticClientFactory
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
            ValidateUrl(url);
            var client = CreateInitialClient(url);
            return client;
        }

        private void ValidateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Provided uri is either null or empty");
            }
        }
    }
}