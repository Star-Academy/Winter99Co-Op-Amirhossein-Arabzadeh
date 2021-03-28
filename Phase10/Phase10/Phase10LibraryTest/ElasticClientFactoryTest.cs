using System;
using System.Collections.Generic;
using ImportRun;
using Microsoft.Extensions.Configuration;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ElasticClientFactoryTest
    {
        [Fact]
        public void CreateElasticClient_ShouldCreateClientProperly_WhenParametersAreValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            var elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.HttpLocalhost);
            Assert.NotNull(elasticClient);
        }

        public static IEnumerable<object[]> CreateElasticClientInvalidArguments = new List<object[]>
        {
            new object[] {"   "},
            new object[] {null},
        };

        [Theory]
        [MemberData(nameof(CreateElasticClientInvalidArguments))]
        public void Doc_ShouldThrowArgumentException_WhenParametersAreInvalid(string uri)
        {
            var elasticClientFactory = new ElasticClientFactory();
            void Action() => elasticClientFactory.CreateElasticClient(uri);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}