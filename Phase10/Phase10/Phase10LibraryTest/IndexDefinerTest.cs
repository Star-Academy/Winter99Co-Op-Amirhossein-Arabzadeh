using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Phase10Library;
using Xunit;

namespace Phase10LibraryTest
{
    public class IndexDefinerTest
    {
        private const string AppsettingsJson = "test-appsettings.json";

        [Fact]
        public void CreateIndex_ShouldCreateIndexMatchingByWantedElasticQuery_WhenParameterIsValid()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(AppsettingsJson, false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            const string indexName = "my_docs_test";
            var uri = new Uri(settings.Addresses.host);
            IElasticClient client = new ElasticClient(uri);
            var elasticResponseValidator = new ElasticResponseValidator();
            var indexDefiner = new IndexDefiner(client, elasticResponseValidator, settings);
            indexDefiner.CreateIndex(indexName);
            var response = client.LowLevel.Indices.GetMapping<StringResponse>("my_docs_test");
            const string expectedMapping = "{\n  \"my_docs_test\" : {\n" +
                                           "    \"mappings\" : {\n " +
                                           "     \"properties\" : {\n" +
                                           "        \"content\" : {\n" +
                                           "          \"type\" : \"text\",\n" +
                                           "          \"fields\" : {\n" +
                                           "            \"content\" : {\n" +
                                           "              \"type\" : \"text\",\n" +
                                           "              \"analyzer\" : \"my-ngram-analyzer\"\n" +
                                           "            },\n            \"keyword\" : {\n" +
                                           "              \"type\" : \"keyword\"\n" +
                                           "            }\n          }\n        },\n        \"name\" : {\n" +
                                           "          \"type\" : \"long\"\n" +
                                           "        }\n      }\n    }\n  }\n}";
            var trimmedExpectedMapping = string.Concat(expectedMapping.Where(c => !Char.IsWhiteSpace(c)));
            var trimmedResponseBody = string.Concat(response.Body.Where(c => !Char.IsWhiteSpace(c)));
            
            Assert.Equal(trimmedExpectedMapping, trimmedResponseBody);
        }

        private const string SomeStringWithCapitalCharacter = "SomeStringWithCapitalCharacter";
        private const string WhiteSpace = "    ";
        public static IEnumerable<object[]> CreateIndexInvalidArguments = new List<object[]>
        {
            new object[] {null},
            new object[] {WhiteSpace},
            new object[] {SomeStringWithCapitalCharacter},
        };
        
        [Theory]
        [MemberData(nameof(CreateIndexInvalidArguments))]
        public void CreateIndex_ShouldThrowArgumentException_WhenParametersAreInvalid(string indexName)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            var url = new Uri(settings.Addresses.host);
            IElasticClient client = new ElasticClient(url);
            
            var elasticResponseValidator = new ElasticResponseValidator();
            
            var indexDefiner = new IndexDefiner(client, elasticResponseValidator, settings);
            
            void Action() => indexDefiner.CreateIndex(indexName);
            Assert.Throws<ArgumentException>(Action);
        }
    }
}