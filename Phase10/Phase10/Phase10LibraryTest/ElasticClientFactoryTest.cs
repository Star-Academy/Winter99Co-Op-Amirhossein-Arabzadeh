using System;
using System.Collections.Generic;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class ElasticClientFactoryTest
    {
        [Fact]
        public void CreateElasticClient_ShouldCreateClientProperly_WhenParametersAreValid()
        {
            ElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var elasticClient = elasticClientFactory.CreateElasticClient(Addresses.HttpLocalhost);
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
            ElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            void Action() => elasticClientFactory.CreateElasticClient(uri);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}